using Microsoft.EntityFrameworkCore.Migrations;

namespace EdtechTest2.Migrations
{
    public partial class tftg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Temp",
                table: "Courses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Temp",
                table: "Courses");
        }
    }
}
