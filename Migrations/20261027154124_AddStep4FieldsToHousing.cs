using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JREST.Migrations
{
    /// <inheritdoc />
    public partial class AddStep4FieldsToHousing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuildingNumber",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Governorate",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HousingType",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Neighborhood",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuildingNumber",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "City",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "Governorate",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "HousingType",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "Neighborhood",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "HousingApplications");
        }
    }
}
