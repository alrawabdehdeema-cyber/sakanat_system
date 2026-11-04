using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JREST.Migrations
{
    /// <inheritdoc />
    public partial class AddStep5FieldsToHousing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AllergyDetails",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisabilityDetails",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasAllergy",
                table: "HousingApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDisability",
                table: "HousingApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "HealthInsurance",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicalCareNeeded",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicineDetails",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NeedsMedicine",
                table: "HousingApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllergyDetails",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "DisabilityDetails",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "HasAllergy",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "HasDisability",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "HealthInsurance",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "MedicalCareNeeded",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "MedicineDetails",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "NeedsMedicine",
                table: "HousingApplications");
        }
    }
}
