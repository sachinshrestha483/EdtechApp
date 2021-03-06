using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EdtechTest2.Migrations
{
    public partial class changeintr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "refundDate",
                table: "CourseTransictionRecords",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "refundReason",
                table: "CourseTransictionRecords",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "refundDate",
                table: "CourseTransictionRecords");

            migrationBuilder.DropColumn(
                name: "refundReason",
                table: "CourseTransictionRecords");
        }
    }
}
