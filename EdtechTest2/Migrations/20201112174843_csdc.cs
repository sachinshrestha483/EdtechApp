using Microsoft.EntityFrameworkCore.Migrations;

namespace EdtechTest2.Migrations
{
    public partial class csdc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTransictionRecords_Coupons_CouponId",
                table: "CourseTransictionRecords");

            migrationBuilder.AlterColumn<int>(
                name: "CouponId",
                table: "CourseTransictionRecords",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTransictionRecords_Coupons_CouponId",
                table: "CourseTransictionRecords",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTransictionRecords_Coupons_CouponId",
                table: "CourseTransictionRecords");

            migrationBuilder.AlterColumn<int>(
                name: "CouponId",
                table: "CourseTransictionRecords",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTransictionRecords_Coupons_CouponId",
                table: "CourseTransictionRecords",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
