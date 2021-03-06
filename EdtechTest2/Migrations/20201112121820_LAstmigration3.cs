using Microsoft.EntityFrameworkCore.Migrations;

namespace EdtechTest2.Migrations
{
    public partial class LAstmigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseTransictions",
                table: "CourseTransictions");

            migrationBuilder.RenameTable(
                name: "CourseTransictions",
                newName: "CourseTransictionRecords");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseTransictionRecords",
                table: "CourseTransictionRecords",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseTransictionRecords",
                table: "CourseTransictionRecords");

            migrationBuilder.RenameTable(
                name: "CourseTransictionRecords",
                newName: "CourseTransictions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseTransictions",
                table: "CourseTransictions",
                column: "id");
        }
    }
}
