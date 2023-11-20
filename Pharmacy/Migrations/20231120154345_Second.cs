using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "Products",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 11, 20, 17, 43, 44, 959, DateTimeKind.Local).AddTicks(760),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 11, 20, 17, 32, 22, 684, DateTimeKind.Local).AddTicks(8632));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Consumables_ExpirationDate",
                table: "Products",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2028, 11, 20, 17, 43, 44, 958, DateTimeKind.Local).AddTicks(9788),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2028, 11, 20, 17, 32, 22, 684, DateTimeKind.Local).AddTicks(7609));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "UPC",
                keyValue: 1234567890L,
                column: "Consumables_ExpirationDate",
                value: new DateTime(2028, 11, 20, 17, 43, 44, 958, DateTimeKind.Local).AddTicks(9788));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "Products",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 11, 20, 17, 32, 22, 684, DateTimeKind.Local).AddTicks(8632),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 11, 20, 17, 43, 44, 959, DateTimeKind.Local).AddTicks(760));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Consumables_ExpirationDate",
                table: "Products",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2028, 11, 20, 17, 32, 22, 684, DateTimeKind.Local).AddTicks(7609),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2028, 11, 20, 17, 43, 44, 958, DateTimeKind.Local).AddTicks(9788));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "UPC",
                keyValue: 1234567890L,
                column: "Consumables_ExpirationDate",
                value: new DateTime(2028, 11, 20, 17, 32, 22, 684, DateTimeKind.Local).AddTicks(7609));
        }
    }
}
