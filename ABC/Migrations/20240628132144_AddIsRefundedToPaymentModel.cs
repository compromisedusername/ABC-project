using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABC.Migrations
{
    /// <inheritdoc />
    public partial class AddIsRefundedToPaymentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isRefunded",
                table: "Payment",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                columns: new[] { "Date", "isRefunded" },
                values: new object[] { new DateTime(2024, 6, 28, 15, 21, 43, 810, DateTimeKind.Local).AddTicks(3225), false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isRefunded",
                table: "Payment");

            migrationBuilder.UpdateData(
                table: "Contract",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateFrom", "DateTo" },
                values: new object[] { new DateTime(2024, 6, 27, 23, 24, 26, 256, DateTimeKind.Local).AddTicks(735), new DateTime(2024, 7, 17, 23, 24, 26, 256, DateTimeKind.Local).AddTicks(1230) });

            migrationBuilder.UpdateData(
                table: "Discount",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateFrom", "DateTo" },
                values: new object[] { new DateTime(2024, 6, 26, 23, 24, 26, 251, DateTimeKind.Local).AddTicks(4974), new DateTime(2024, 7, 7, 23, 24, 26, 255, DateTimeKind.Local).AddTicks(1027) });

            migrationBuilder.UpdateData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 6, 27, 23, 24, 26, 255, DateTimeKind.Local).AddTicks(7871));
        }
    }
}
