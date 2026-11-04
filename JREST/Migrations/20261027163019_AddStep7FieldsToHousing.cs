using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JREST.Migrations
{
    /// <inheritdoc />
    public partial class AddStep7FieldsToHousing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowDayTrips",
                table: "HousingApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AllowMultiDayTrips",
                table: "HousingApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AllowTravelAbroad",
                table: "HousingApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AllowWeekendLeave",
                table: "HousingApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowDayTrips",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "AllowMultiDayTrips",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "AllowTravelAbroad",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "AllowWeekendLeave",
                table: "HousingApplications");
        }
    }
}
