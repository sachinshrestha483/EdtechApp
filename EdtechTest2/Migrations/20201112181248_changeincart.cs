using Microsoft.EntityFrameworkCore.Migrations;

namespace EdtechTest2.Migrations
{
    public partial class changeincart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isPurchased",
                table: "UserCourseCarts");

            migrationBuilder.AddColumn<bool>(
                name: "isDone",
                table: "UserCourseCarts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDone",
                table: "UserCourseCarts");

            migrationBuilder.AddColumn<bool>(
                name: "isPurchased",
                table: "UserCourseCarts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
