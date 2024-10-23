using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace conanPlusWebApiApp.Dal.Migrations
{
    /// <inheritdoc />
    public partial class vid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "promoVideos");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2024, 10, 19, 5, 21, 42, 637, DateTimeKind.Local).AddTicks(5185));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2024, 10, 19, 5, 21, 42, 637, DateTimeKind.Local).AddTicks(5191));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$PNQWbFQPVhR768Mwn/OIOucmmJnxf0eb98.UVbCOlo.NT4gLNrxmm");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "promoVideos",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2024, 10, 19, 5, 0, 48, 680, DateTimeKind.Local).AddTicks(5982));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2024, 10, 19, 5, 0, 48, 680, DateTimeKind.Local).AddTicks(5989));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$GP3I.kWCvq0.4XQ2QTJDvObMrEWbuEWTFalOQNCV6vaiHCRJ4H.fu");
        }
    }
}
