using Microsoft.EntityFrameworkCore;
using Npgsql;
using PracticalMicroservices.Events.Entities;
using System.Data;

namespace PracticalMicroservices.Events.Infrastructure.Db
{
    public partial class MessageStoreContext : DbContext
    {
        public MessageStoreContext()
        {
        }

        public MessageStoreContext(DbContextOptions<MessageStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CategoryTypeSummary> CategoryTypeSummaries { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<StreamSummary> StreamSummaries { get; set; }
        public virtual DbSet<StreamTypeSummary> StreamTypeSummaries { get; set; }
        public virtual DbSet<TypeCategorySummary> TypeCategorySummaries { get; set; }
        public virtual DbSet<TypeStreamSummary> TypeStreamSummaries { get; set; }
        public virtual DbSet<TypeSummary> TypeSummaries { get; set; }

        public long WriteMessage(string id, string streamName, string type, string data, string metaData, long expectedVersion) 
        {
            var sql = @"SELECT message_store.write_message(
            	@Id, 
            	@StreamName, 
            	@Type, 
            	@Data, 
            	@MetaData, 
            	null
            )";
            var parameters = new[] 
            { 
                new NpgsqlParameter 
                { 
                    Direction = System.Data.ParameterDirection.Input, 
                    NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar,
                    NpgsqlValue = id.ToString(),
                    ParameterName = "Id"
                },
                new NpgsqlParameter
                {
                    Direction = System.Data.ParameterDirection.Input,
                    NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar,
                    NpgsqlValue = streamName,
                    ParameterName = "StreamName"
                },
                new NpgsqlParameter
                {
                    Direction = System.Data.ParameterDirection.Input,
                    NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar,
                    NpgsqlValue = type,
                    ParameterName = "Type"
                },
                new NpgsqlParameter
                {
                    Direction = System.Data.ParameterDirection.Input,
                    NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb,
                    NpgsqlValue = data,
                    ParameterName = "Data"
                },
                new NpgsqlParameter
                {
                    Direction = System.Data.ParameterDirection.Input,
                    NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb,
                    NpgsqlValue = metaData,
                    ParameterName = "MetaData"
                }
                //new NpgsqlParameter
                //{
                //    Direction = System.Data.ParameterDirection.Input,
                //    NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bigint,
                //    NpgsqlValue = expectedVersion,
                //    ParameterName = "ExpectedVersion"
                //}
            };
            using(var command = Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = sql;
                foreach(var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
                if (command.Connection.State == ConnectionState.Closed)
                    command.Connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            return reader.GetInt64(0);
                        }
                    }
                    return -1;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pgcrypto")
                .HasAnnotation("Relational:Collation", "English_United Kingdom.1252");

            modelBuilder.Entity<CategoryTypeSummary>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("category_type_summary", "message_store");

                entity.Property(e => e.Category)
                    .HasColumnType("character varying")
                    .HasColumnName("category");

                entity.Property(e => e.MessageCount).HasColumnName("message_count");

                entity.Property(e => e.Percent).HasColumnName("percent");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.HasDbFunction(typeof(MessageStoreContext)
                                       .GetMethod(nameof(WriteMessage)))
                                       .HasName("write_message");

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.GlobalPosition)
                    .HasName("messages_pkey");

                entity.ToTable("messages", "message_store");

                entity.HasIndex(e => e.Id, "messages_id")
                    .IsUnique();

                entity.HasIndex(e => new { e.StreamName, e.Position }, "messages_stream")
                    .IsUnique();

                entity.Property(e => e.GlobalPosition).HasColumnName("global_position");

                entity.Property(e => e.Data)
                    .HasColumnType("jsonb")
                    .HasColumnName("data");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.Metadata)
                    .HasColumnType("jsonb")
                    .HasColumnName("metadata");

                entity.Property(e => e.Position).HasColumnName("position");

                entity.Property(e => e.StreamName)
                    .IsRequired()
                    .HasColumnName("stream_name");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasDefaultValueSql("(now() AT TIME ZONE 'utc'::text)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type");
            });

            modelBuilder.Entity<StreamSummary>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("stream_summary", "message_store");

                entity.Property(e => e.MessageCount).HasColumnName("message_count");

                entity.Property(e => e.Percent).HasColumnName("percent");

                entity.Property(e => e.StreamName).HasColumnName("stream_name");
            });

            modelBuilder.Entity<StreamTypeSummary>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("stream_type_summary", "message_store");

                entity.Property(e => e.MessageCount).HasColumnName("message_count");

                entity.Property(e => e.Percent).HasColumnName("percent");

                entity.Property(e => e.StreamName).HasColumnName("stream_name");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<TypeCategorySummary>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("type_category_summary", "message_store");

                entity.Property(e => e.Category)
                    .HasColumnType("character varying")
                    .HasColumnName("category");

                entity.Property(e => e.MessageCount).HasColumnName("message_count");

                entity.Property(e => e.Percent).HasColumnName("percent");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<TypeStreamSummary>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("type_stream_summary", "message_store");

                entity.Property(e => e.MessageCount).HasColumnName("message_count");

                entity.Property(e => e.Percent).HasColumnName("percent");

                entity.Property(e => e.StreamName).HasColumnName("stream_name");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<TypeSummary>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("type_summary", "message_store");

                entity.Property(e => e.MessageCount).HasColumnName("message_count");

                entity.Property(e => e.Percent).HasColumnName("percent");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
