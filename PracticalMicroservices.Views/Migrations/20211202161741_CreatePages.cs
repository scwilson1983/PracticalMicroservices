using Microsoft.EntityFrameworkCore.Migrations;

namespace PracticalMicroservices.MaterializedViews.Migrations
{
    public partial class CreatePages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "Pages",
                schema: "public",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Name);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pages",
                schema: "public");
        }
    }
}
