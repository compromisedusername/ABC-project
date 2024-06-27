using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABC.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedToClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Discount_IdDiscount",
                table: "Contract");

            migrationBuilder.AlterColumn<int>(
                name: "IdDiscount",
                table: "Contract",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Discount_IdDiscount",
                table: "Contract",
                column: "IdDiscount",
                principalTable: "Discount",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Discount_IdDiscount",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Client");

            migrationBuilder.AlterColumn<int>(
                name: "IdDiscount",
                table: "Contract",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Contract",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateFrom", "DateTo" },
                values: new object[] { new DateTime(2024, 6, 27, 18, 51, 34, 176, DateTimeKind.Local).AddTicks(6341), new DateTime(2024, 7, 17, 18, 51, 34, 176, DateTimeKind.Local).AddTicks(6740) });

            migrationBuilder.UpdateData(
                table: "Discount",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateFrom", "DateTo" },
                values: new object[] { new DateTime(2024, 6, 26, 18, 51, 34, 172, DateTimeKind.Local).AddTicks(1077), new DateTime(2024, 7, 7, 18, 51, 34, 175, DateTimeKind.Local).AddTicks(7247) });

            migrationBuilder.UpdateData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 6, 27, 18, 51, 34, 176, DateTimeKind.Local).AddTicks(3564));

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Discount_IdDiscount",
                table: "Contract",
                column: "IdDiscount",
                principalTable: "Discount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
