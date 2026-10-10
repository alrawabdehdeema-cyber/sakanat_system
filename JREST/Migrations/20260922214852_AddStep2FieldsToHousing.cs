using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JREST.Migrations
{
    /// <inheritdoc />
    public partial class AddStep2FieldsToHousing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "HousingApplications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EnrollmentDate",
                table: "HousingApplications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaritalStatus",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParentPhone",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PassportNumber",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Religion",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentPhone",
                table: "HousingApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "EnrollmentDate",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "ParentPhone",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "PassportNumber",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "Religion",
                table: "HousingApplications");

            migrationBuilder.DropColumn(
                name: "StudentPhone",
                table: "HousingApplications");

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
        }
    }
}
