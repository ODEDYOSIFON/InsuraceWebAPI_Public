using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsuranceWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "John Doe" },
                    { 2, "jane.smith@example.com", "Jane Smith" }
                });

            migrationBuilder.InsertData(
                table: "InsurancePolicies",
                columns: new[] { "ID", "EndDate", "InsuranceAmount", "PolicyNumber", "StartDate", "UserID" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 20, 14, 6, 30, 673, DateTimeKind.Local).AddTicks(130), 50000.0, "POL123456", new DateTime(2024, 6, 20, 14, 6, 30, 673, DateTimeKind.Local).AddTicks(72), 1 },
                    { 2, new DateTime(2025, 6, 20, 14, 6, 30, 673, DateTimeKind.Local).AddTicks(138), 75000.0, "POL654321", new DateTime(2024, 6, 20, 14, 6, 30, 673, DateTimeKind.Local).AddTicks(137), 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InsurancePolicies",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "InsurancePolicies",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 2);
        }
    }
}
