using Microsoft.EntityFrameworkCore;
using PracticalMicroservices.MaterializedViews.Entities;

namespace PracticalMicroservices.MaterializedViews.Infrastructure
{
    public class MaterializedViewsContext : DbContext
    {
        public DbSet<Page> Pages { get; set; }

        public MaterializedViewsContext()
        {
        }

        public MaterializedViewsContext(DbContextOptions<MaterializedViewsContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseNpgsql("Host=localhost;User Id=postgres;Password=Wynyard_2020;Port=5432;Database=postgres;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}
