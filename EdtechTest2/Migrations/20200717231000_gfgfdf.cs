using Microsoft.EntityFrameworkCore.Migrations;

namespace EdtechTest2.Migrations
{
    public partial class gfgfdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LectureText",
                table: "Contents");

            migrationBuilder.AddColumn<string>(
                name: "LectureArticleText",
                table: "Contents",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDownlodableContent",
                table: "Contents",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LectureArticleText",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "isDownlodableContent",
                table: "Contents");

            migrationBuilder.AddColumn<string>(
                name: "LectureText",
                table: "Contents",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
