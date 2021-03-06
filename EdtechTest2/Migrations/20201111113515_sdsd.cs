using Microsoft.EntityFrameworkCore.Migrations;

namespace EdtechTest2.Migrations
{
    public partial class sdsd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseCarts_Coupons_CouponId",
                table: "CourseCarts");

            migrationBuilder.AlterColumn<int>(
                name: "CouponId",
                table: "CourseCarts",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCarts_Coupons_CouponId",
                table: "CourseCarts",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseCarts_Coupons_CouponId",
                table: "CourseCarts");

            migrationBuilder.AlterColumn<int>(
                name: "CouponId",
                table: "CourseCarts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCarts_Coupons_CouponId",
                table: "CourseCarts",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
