using Microsoft.EntityFrameworkCore.Migrations;

namespace EdtechTest2.Migrations
{
    public partial class AddNewPropertieyInCoup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CouponId",
                table: "CourseCarts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseCarts_CouponId",
                table: "CourseCarts",
                column: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCarts_Coupons_CouponId",
                table: "CourseCarts",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseCarts_Coupons_CouponId",
                table: "CourseCarts");

            migrationBuilder.DropIndex(
                name: "IX_CourseCarts_CouponId",
                table: "CourseCarts");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "CourseCarts");
        }
    }
}
