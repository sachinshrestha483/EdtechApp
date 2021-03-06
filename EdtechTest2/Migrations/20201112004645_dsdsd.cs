using Microsoft.EntityFrameworkCore.Migrations;

namespace EdtechTest2.Migrations
{
    public partial class dsdsd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isRefunded",
                table: "PurchasedCourses");

            migrationBuilder.AddColumn<bool>(
                name: "isRefund",
                table: "PurchasedCourses",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isRefund",
                table: "PurchasedCourses");

            migrationBuilder.AddColumn<bool>(
                name: "isRefunded",
                table: "PurchasedCourses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
