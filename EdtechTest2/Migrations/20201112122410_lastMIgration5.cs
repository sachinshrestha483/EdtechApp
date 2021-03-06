using Microsoft.EntityFrameworkCore.Migrations;

namespace EdtechTest2.Migrations
{
    public partial class lastMIgration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "CourseTransictionRecords",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseTransictionRecords_ApplicationUserId",
                table: "CourseTransictionRecords",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTransictionRecords_AspNetUsers_ApplicationUserId",
                table: "CourseTransictionRecords",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTransictionRecords_AspNetUsers_ApplicationUserId",
                table: "CourseTransictionRecords");

            migrationBuilder.DropIndex(
                name: "IX_CourseTransictionRecords_ApplicationUserId",
                table: "CourseTransictionRecords");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "CourseTransictionRecords");
        }
    }
}
