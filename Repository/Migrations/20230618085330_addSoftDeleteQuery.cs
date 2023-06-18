using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class addSoftDeleteQuery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_DeletedAt",
                table: "Users",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Services_DeletedAt",
                table: "Services",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Qualification_DeletedAt",
                table: "Qualification",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DeletedAt",
                table: "Prescriptions",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistory_DeletedAt",
                table: "MedicalHistory",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_DeletedAt",
                table: "Feedbacks",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorLogTimes_DeletedAt",
                table: "DoctorLogTimes",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorInfos_DeletedAt",
                table: "DoctorInfos",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Birds_DeletedAt",
                table: "Birds",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DeletedAt",
                table: "Appointments",
                column: "DeletedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_DeletedAt",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Services_DeletedAt",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Qualification_DeletedAt",
                table: "Qualification");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_DeletedAt",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_MedicalHistory_DeletedAt",
                table: "MedicalHistory");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_DeletedAt",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_DoctorLogTimes_DeletedAt",
                table: "DoctorLogTimes");

            migrationBuilder.DropIndex(
                name: "IX_DoctorInfos_DeletedAt",
                table: "DoctorInfos");

            migrationBuilder.DropIndex(
                name: "IX_Birds_DeletedAt",
                table: "Birds");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DeletedAt",
                table: "Appointments");
        }
    }
}
