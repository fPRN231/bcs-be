using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class changeAppointmentIdFeedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Appointments_AppointmentId1",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_AppointmentId1",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "AppointmentId1",
                table: "Feedbacks");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppointmentId",
                table: "Feedbacks",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_AppointmentId",
                table: "Feedbacks",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Appointments_AppointmentId",
                table: "Feedbacks",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Appointments_AppointmentId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_AppointmentId",
                table: "Feedbacks");

            migrationBuilder.AlterColumn<string>(
                name: "AppointmentId",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "AppointmentId1",
                table: "Feedbacks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_AppointmentId1",
                table: "Feedbacks",
                column: "AppointmentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Appointments_AppointmentId1",
                table: "Feedbacks",
                column: "AppointmentId1",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
