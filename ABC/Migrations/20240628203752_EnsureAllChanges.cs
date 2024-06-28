using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABC.Migrations
{
    /// <inheritdoc />
    public partial class EnsureAllChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Contract",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateFrom", "DateTo" },
                values: new object[] { new DateTime(2024, 6, 28, 22, 37, 51, 524, DateTimeKind.Local).AddTicks(3604), new DateTime(2024, 7, 18, 22, 37, 51, 524, DateTimeKind.Local).AddTicks(4008) });

            migrationBuilder.UpdateData(
                table: "Discount",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateFrom", "DateTo" },
                values: new object[] { new DateTime(2024, 6, 27, 22, 37, 51, 519, DateTimeKind.Local).AddTicks(8096), new DateTime(2024, 7, 8, 22, 37, 51, 523, DateTimeKind.Local).AddTicks(4293) });

            migrationBuilder.UpdateData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 6, 28, 22, 37, 51, 524, DateTimeKind.Local).AddTicks(837));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "IdUser", "Email", "Login", "Password", "RefreshToken", "RefreshTokenExp", "Salt" },
                values: new object[] { 1, "admin@abc.pl", "admin", "UCj/SzNluTr2O7t1unmdXEPX3VpOkxqWUUMrhfwefiA=", "gdaodTn6fZrAFZnvZhKLnabnBVlyE6/lJTlXcfpR3EI=", null, "s1q1vyXNvCGXtzXswP6GUg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "IdUser",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Contract",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateFrom", "DateTo" },
                values: new object[] { new DateTime(2024, 6, 28, 15, 21, 43, 810, DateTimeKind.Local).AddTicks(6220), new DateTime(2024, 7, 18, 15, 21, 43, 810, DateTimeKind.Local).AddTicks(6643) });

            migrationBuilder.UpdateData(
                table: "Discount",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateFrom", "DateTo" },
                values: new object[] { new DateTime(2024, 6, 27, 15, 21, 43, 806, DateTimeKind.Local).AddTicks(566), new DateTime(2024, 7, 8, 15, 21, 43, 809, DateTimeKind.Local).AddTicks(6504) });

            migrationBuilder.UpdateData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 6, 28, 15, 21, 43, 810, DateTimeKind.Local).AddTicks(3225));
        }
    }
}
