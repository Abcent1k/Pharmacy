using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pharmacy.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    UPC = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EDRPOU = table.Column<long>(type: "bigint", nullable: false),
                    ProductType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Consumables_ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2028, 11, 20, 17, 32, 22, 684, DateTimeKind.Local).AddTicks(7609)),
                    ConsumableType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2024, 11, 20, 17, 32, 22, 684, DateTimeKind.Local).AddTicks(8632)),
                    DrugType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NeedRecipe = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.UPC);
                    table.CheckConstraint("EDRPOU", "LEN(EDRPOU) = 8");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => new { x.Id, x.UserId });
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "InventoryProducts",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductUPC = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryProducts", x => new { x.OrderId, x.UserId, x.ProductUPC });
                    table.ForeignKey(
                        name: "FK_InventoryProducts_Orders_OrderId_UserId",
                        columns: x => new { x.OrderId, x.UserId },
                        principalTable: "Orders",
                        principalColumns: new[] { "Id", "UserId" });
                    table.ForeignKey(
                        name: "FK_InventoryProducts_Products_ProductUPC",
                        column: x => x.ProductUPC,
                        principalTable: "Products",
                        principalColumn: "UPC",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryProducts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "UPC", "DeviceType", "EDRPOU", "Name", "Price", "ProductType" },
                values: new object[] { 986235712L, "Inhaler", 11111111L, "Інгалятор", 850.25m, "Devices" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "UPC", "ConsumableType", "EDRPOU", "Name", "Price", "ProductType" },
                values: new object[] { 1234567890L, "Syringe", 88888888L, "Mediprop", 250.5m, "Consumables" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, "Яна", "Алексюк" },
                    { 2, "Євгеній", "Жидик" },
                    { 5, "Ясос", "Біба" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "UserId", "OrderDate", "TotalPrice" },
                values: new object[] { 1, 1, null, null });

            migrationBuilder.InsertData(
                table: "InventoryProducts",
                columns: new[] { "OrderId", "ProductUPC", "UserId", "Quantity" },
                values: new object[] { 1, 1234567890L, 1, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryProducts_ProductUPC",
                table: "InventoryProducts",
                column: "ProductUPC");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryProducts_UserId",
                table: "InventoryProducts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryProducts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
