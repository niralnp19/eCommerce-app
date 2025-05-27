using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerce.Data.Migrations
{
    public partial class UpdatedAdImageFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AdId",
                table: "AdImage",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "AdImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "AdImage",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "AdImage",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AdImage_AdId",
                table: "AdImage",
                column: "AdId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdImage_Ad_AdId",
                table: "AdImage",
                column: "AdId",
                principalTable: "Ad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdImage_Ad_AdId",
                table: "AdImage");

            migrationBuilder.DropIndex(
                name: "IX_AdImage_AdId",
                table: "AdImage");

            migrationBuilder.DropColumn(
                name: "AdId",
                table: "AdImage");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "AdImage");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "AdImage");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AdImage");
        }
    }
}
