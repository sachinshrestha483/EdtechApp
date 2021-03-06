using Microsoft.EntityFrameworkCore.Migrations;

namespace EdtechTest2.Migrations
{
    public partial class fehgfeu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseRequirements_AspNetUsers_ApplicationUserId1",
                table: "CourseRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseWhatWillYouLearns_AspNetUsers_ApplicationUserId1",
                table: "CourseWhatWillYouLearns");

            migrationBuilder.DropIndex(
                name: "IX_CourseWhatWillYouLearns_ApplicationUserId1",
                table: "CourseWhatWillYouLearns");

            migrationBuilder.DropIndex(
                name: "IX_CourseRequirements_ApplicationUserId1",
                table: "CourseRequirements");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "CourseWhatWillYouLearns");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "CourseWhatWillYouLearns");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "CourseRequirements");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "CourseRequirements");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "CourseWhatWillYouLearns",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "CourseRequirements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseWhatWillYouLearns_CourseId",
                table: "CourseWhatWillYouLearns",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRequirements_CourseId",
                table: "CourseRequirements",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseRequirements_Courses_CourseId",
                table: "CourseRequirements",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseWhatWillYouLearns_Courses_CourseId",
                table: "CourseWhatWillYouLearns",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseRequirements_Courses_CourseId",
                table: "CourseRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseWhatWillYouLearns_Courses_CourseId",
                table: "CourseWhatWillYouLearns");

            migrationBuilder.DropIndex(
                name: "IX_CourseWhatWillYouLearns_CourseId",
                table: "CourseWhatWillYouLearns");

            migrationBuilder.DropIndex(
                name: "IX_CourseRequirements_CourseId",
                table: "CourseRequirements");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CourseWhatWillYouLearns");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CourseRequirements");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "CourseWhatWillYouLearns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "CourseWhatWillYouLearns",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "CourseRequirements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "CourseRequirements",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseWhatWillYouLearns_ApplicationUserId1",
                table: "CourseWhatWillYouLearns",
                column: "ApplicationUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRequirements_ApplicationUserId1",
                table: "CourseRequirements",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseRequirements_AspNetUsers_ApplicationUserId1",
                table: "CourseRequirements",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseWhatWillYouLearns_AspNetUsers_ApplicationUserId1",
                table: "CourseWhatWillYouLearns",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
