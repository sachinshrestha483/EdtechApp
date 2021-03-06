using Microsoft.EntityFrameworkCore.Migrations;

namespace EdtechTest2.Migrations
{
    public partial class LAstMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "CourseTransictionRecords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseTransictionRecords_CourseId",
                table: "CourseTransictionRecords",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTransictionRecords_Courses_CourseId",
                table: "CourseTransictionRecords",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTransictionRecords_Courses_CourseId",
                table: "CourseTransictionRecords");

            migrationBuilder.DropIndex(
                name: "IX_CourseTransictionRecords_CourseId",
                table: "CourseTransictionRecords");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CourseTransictionRecords");
        }
    }
}
