using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABC.Migrations
{
    /// <inheritdoc />
    public partial class FixIsDeletedInClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Client");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ClientNatural",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "ClientNatural",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Contract",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateFrom", "DateTo" },
                values: new object[] { new DateTime(2024, 6, 27, 20, 11, 35, 381, DateTimeKind.Local).AddTicks(4257), new DateTime(2024, 7, 17, 20, 11, 35, 381, DateTimeKind.Local).AddTicks(4676) });

            migrationBuilder.UpdateData(
                table: "Discount",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateFrom", "DateTo" },
                values: new object[] { new DateTime(2024, 6, 26, 20, 11, 35, 376, DateTimeKind.Local).AddTicks(8273), new DateTime(2024, 7, 7, 20, 11, 35, 380, DateTimeKind.Local).AddTicks(4651) });

            migrationBuilder.UpdateData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 6, 27, 20, 11, 35, 381, DateTimeKind.Local).AddTicks(1268));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ClientNatural");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Client",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Client",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Client",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Contract",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateFrom", "DateTo" },
                values: new object[] { new DateTime(2024, 6, 27, 19, 47, 28, 902, DateTimeKind.Local).AddTicks(894), new DateTime(2024, 7, 17, 19, 47, 28, 902, DateTimeKind.Local).AddTicks(1315) });

            migrationBuilder.UpdateData(
                table: "Discount",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateFrom", "DateTo" },
                values: new object[] { new DateTime(2024, 6, 26, 19, 47, 28, 897, DateTimeKind.Local).AddTicks(1370), new DateTime(2024, 7, 7, 19, 47, 28, 901, DateTimeKind.Local).AddTicks(745) });

            migrationBuilder.UpdateData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 6, 27, 19, 47, 28, 901, DateTimeKind.Local).AddTicks(8038));
        }
    }
}
