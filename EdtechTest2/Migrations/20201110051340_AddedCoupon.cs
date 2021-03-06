using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EdtechTest2.Migrations
{
    public partial class AddedCoupon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(nullable: false),
                    couponCode = table.Column<string>(nullable: true),
                    numberOfcouponAlloted = table.Column<int>(nullable: false),
                    numberofcouponUnUsed = table.Column<int>(nullable: false),
                    validTill = table.Column<DateTime>(nullable: false),
                    discountMethod = table.Column<int>(nullable: false),
                    discountValue = table.Column<int>(nullable: false),
                    coupounCreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.id);
                    table.ForeignKey(
                        name: "FK_Coupons_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_CourseId",
                table: "Coupons",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupons");
        }
    }
}
