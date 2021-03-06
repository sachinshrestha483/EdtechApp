using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EdtechTest2.Migrations
{
    public partial class lastmigration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CouponId",
                table: "CourseTransictionRecords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "price",
                table: "CourseTransictionRecords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "purchaseDate",
                table: "CourseTransictionRecords",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_CourseTransictionRecords_CouponId",
                table: "CourseTransictionRecords",
                column: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTransictionRecords_Coupons_CouponId",
                table: "CourseTransictionRecords",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTransictionRecords_Coupons_CouponId",
                table: "CourseTransictionRecords");

            migrationBuilder.DropIndex(
                name: "IX_CourseTransictionRecords_CouponId",
                table: "CourseTransictionRecords");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "CourseTransictionRecords");

            migrationBuilder.DropColumn(
                name: "price",
                table: "CourseTransictionRecords");

            migrationBuilder.DropColumn(
                name: "purchaseDate",
                table: "CourseTransictionRecords");
        }
    }
}
