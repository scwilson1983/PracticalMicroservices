using Microsoft.EntityFrameworkCore.Migrations;

namespace PracticalMicroservices.MaterializedViews.Migrations
{
    public partial class AddGlobalPositionToPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GlobalPosition",
                schema: "public",
                table: "Pages",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GlobalPosition",
                schema: "public",
                table: "Pages");
        }
    }
}
