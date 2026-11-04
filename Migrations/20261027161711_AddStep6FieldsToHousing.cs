using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JREST.Migrations
{
    /// <inheritdoc />
    public partial class AddStep6FieldsToHousing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasAsthma",
                table: "HousingApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasCancer",
                table: "HousingApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDiabetes",
                table: "HousingApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasEpilepsy",
                table: "HousingApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasHeartDisease",
                table: "HousingApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasHypertension",
                table: "HousingApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasPsychologicalDisorder",
                table: "HousingApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasAsthma",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "HasCancer",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "HasDiabetes",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "HasEpilepsy",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "HasHeartDisease",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "HasHypertension",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "HasPsychologicalDisorder",
                table: "HousingApplications");
        }
    }
}
