using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace conanPlusWebApiApp.Dal.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContactForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "ContactForms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2024, 10, 18, 6, 10, 34, 38, DateTimeKind.Local).AddTicks(3460));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2024, 10, 18, 6, 10, 34, 38, DateTimeKind.Local).AddTicks(3466));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$F/I.II1WpV1Z5x0nPcLkouKCsbwCswwxZ0UYmUVIvhSiSGQTvWhRG");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "ContactForms");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2024, 10, 16, 8, 29, 46, 97, DateTimeKind.Local).AddTicks(1838));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2024, 10, 16, 8, 29, 46, 97, DateTimeKind.Local).AddTicks(1845));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$AdU/j8mbLs95sasj4gDCROdgbCI21GZQsw12R9DRAR2rKnKhc2o5a");
        }
    }
}
