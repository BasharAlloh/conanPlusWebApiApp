using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace conanPlusWebApiApp.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddVid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "promoVideos",
                columns: table => new
                {
                    VideoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VideoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    VideoFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promoVideos", x => x.VideoId);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "promoVideos");

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
    }
}
