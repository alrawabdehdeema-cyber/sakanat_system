using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JREST.Migrations
{
    /// <inheritdoc />
    public partial class AddStep3FieldsToHousing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TawjihiGpa",
                table: "HousingApplications",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Gpa",
                table: "HousingApplications",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AddColumn<string>(
                name: "AcademicLevel",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AcademicYear",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "College",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HousingName",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Penalties",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PledgeParentName",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PledgeStudentName",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudyType",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcademicLevel",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "AcademicYear",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "College",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "HousingName",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "Penalties",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "PledgeParentName",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "PledgeStudentName",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "StudyType",
                table: "HousingApplications");

            migrationBuilder.AlterColumn<decimal>(
                name: "TawjihiGpa",
                table: "HousingApplications",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Gpa",
                table: "HousingApplications",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
