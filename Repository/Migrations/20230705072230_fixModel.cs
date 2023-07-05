using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class fixModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Birds_Users_UserId1",
                table: "Birds");

            migrationBuilder.DropIndex(
                name: "IX_Birds_UserId1",
                table: "Birds");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Birds");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Birds",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Birds_UserId",
                table: "Birds",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Birds_Users_UserId",
                table: "Birds",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Birds_Users_UserId",
                table: "Birds");

            migrationBuilder.DropIndex(
                name: "IX_Birds_UserId",
                table: "Birds");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Birds",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Birds",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Birds_UserId1",
                table: "Birds",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Birds_Users_UserId1",
                table: "Birds",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
