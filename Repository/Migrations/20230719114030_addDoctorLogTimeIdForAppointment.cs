using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class addDoctorLogTimeIdForAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DoctorLogTimeId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorLogTimeId",
                table: "Appointments",
                column: "DoctorLogTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_DoctorLogTimes_DoctorLogTimeId",
                table: "Appointments",
                column: "DoctorLogTimeId",
                principalTable: "DoctorLogTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_DoctorLogTimes_DoctorLogTimeId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorLogTimeId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DoctorLogTimeId",
                table: "Appointments");
        }
    }
}
