using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JREST.Migrations
{
    /// <inheritdoc />
    public partial class AddStep8FilesToHousing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FamilyBookPath",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GuardianIdPath",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HealthCertificatePath",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PowerOfAttorneyPath",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FamilyBookPath",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "GuardianIdPath",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "HealthCertificatePath",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "PowerOfAttorneyPath",
                table: "HousingApplications");
        }
    }
}
