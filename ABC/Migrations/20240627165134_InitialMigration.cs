using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ABC.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    HouseNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdAddress = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Client_Address_IdAddress",
                        column: x => x.IdAddress,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoftwareSystem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VersionInformation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PriceForYear = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    IdCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareSystem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoftwareSystem_Category_IdCategory",
                        column: x => x.IdCategory,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientCompany",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KRS = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCompany", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientCompany_Client_Id",
                        column: x => x.Id,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientNatural",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FristName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PESEL = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientNatural", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientNatural_Client_Id",
                        column: x => x.Id,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    SupportUpdatePeriodInYears = table.Column<int>(type: "int", nullable: false),
                    UpdateInformation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IdDiscount = table.Column<int>(type: "int", nullable: false),
                    IdSoftwareSystem = table.Column<int>(type: "int", nullable: false),
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    IsSigned = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_Client_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contract_Discount_IdDiscount",
                        column: x => x.IdDiscount,
                        principalTable: "Discount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contract_SoftwareSystem_IdSoftwareSystem",
                        column: x => x.IdSoftwareSystem,
                        principalTable: "SoftwareSystem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contarcts_SoftwareSystems",
                columns: table => new
                {
                    IdContract = table.Column<int>(type: "int", nullable: false),
                    IdSoftwareSystem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contarcts_SoftwareSystems", x => new { x.IdContract, x.IdSoftwareSystem });
                    table.ForeignKey(
                        name: "FK_Contarcts_SoftwareSystems_Contract_IdContract",
                        column: x => x.IdContract,
                        principalTable: "Contract",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contarcts_SoftwareSystems_SoftwareSystem_IdSoftwareSystem",
                        column: x => x.IdSoftwareSystem,
                        principalTable: "SoftwareSystem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoneyAmount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdContract = table.Column<int>(type: "int", nullable: false),
                    IdClient = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Client_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payment_Contract_IdContract",
                        column: x => x.IdContract,
                        principalTable: "Contract",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "Country", "HouseNumber", "Street" },
                values: new object[,]
                {
                    { 1, "Warsaw", "Poland", "2", "Nizinna" },
                    { 2, "Warsaw", "Poland", "1", "Fabryczna" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Education", "Educational purposes only." });

            migrationBuilder.InsertData(
                table: "Discount",
                columns: new[] { "Id", "DateFrom", "DateTo", "Name", "Value" },
                values: new object[] { 1, new DateTime(2024, 6, 26, 18, 51, 34, 172, DateTimeKind.Local).AddTicks(1077), new DateTime(2024, 7, 7, 18, 51, 34, 175, DateTimeKind.Local).AddTicks(7247), "Black Friday", 20 });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "Id", "Email", "IdAddress", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "example@example.com", 1, "661354887" },
                    { 2, "example@example2.com", 2, "887345645" }
                });

            migrationBuilder.InsertData(
                table: "SoftwareSystem",
                columns: new[] { "Id", "Description", "IdCategory", "Name", "PriceForYear", "VersionInformation" },
                values: new object[] { 1, "Educational Software for students", 1, "EduSoftware", 2000m, "Version 1.0" });

            migrationBuilder.InsertData(
                table: "ClientCompany",
                columns: new[] { "Id", "CompanyName", "KRS" },
                values: new object[] { 1, "CompanyName", "12345678" });

            migrationBuilder.InsertData(
                table: "ClientNatural",
                columns: new[] { "Id", "FristName", "LastName", "PESEL" },
                values: new object[] { 2, "Jan", "Kowalski", "031276398" });

            migrationBuilder.InsertData(
                table: "Contract",
                columns: new[] { "Id", "DateFrom", "DateTo", "IdClient", "IdDiscount", "IdSoftwareSystem", "IsActive", "IsSigned", "Price", "SupportUpdatePeriodInYears", "UpdateInformation" },
                values: new object[] { 1, new DateTime(2024, 6, 27, 18, 51, 34, 176, DateTimeKind.Local).AddTicks(6341), new DateTime(2024, 7, 17, 18, 51, 34, 176, DateTimeKind.Local).AddTicks(6740), 1, 1, 1, false, false, 3000m, 2, "Possible updates in future" });

            migrationBuilder.InsertData(
                table: "Contarcts_SoftwareSystems",
                columns: new[] { "IdContract", "IdSoftwareSystem" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "Payment",
                columns: new[] { "Id", "Date", "IdClient", "IdContract", "MoneyAmount" },
                values: new object[] { 1, new DateTime(2024, 6, 27, 18, 51, 34, 176, DateTimeKind.Local).AddTicks(3564), 1, 1, 1000m });

            migrationBuilder.CreateIndex(
                name: "IX_Client_IdAddress",
                table: "Client",
                column: "IdAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Contarcts_SoftwareSystems_IdSoftwareSystem",
                table: "Contarcts_SoftwareSystems",
                column: "IdSoftwareSystem");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_IdClient",
                table: "Contract",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_IdDiscount",
                table: "Contract",
                column: "IdDiscount");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_IdSoftwareSystem",
                table: "Contract",
                column: "IdSoftwareSystem");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_IdClient",
                table: "Payment",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_IdContract",
                table: "Payment",
                column: "IdContract");

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareSystem_IdCategory",
                table: "SoftwareSystem",
                column: "IdCategory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientCompany");

            migrationBuilder.DropTable(
                name: "ClientNatural");

            migrationBuilder.DropTable(
                name: "Contarcts_SoftwareSystems");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropTable(
                name: "SoftwareSystem");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
