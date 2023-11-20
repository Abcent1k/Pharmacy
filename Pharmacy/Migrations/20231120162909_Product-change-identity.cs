using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Migrations
{
    /// <inheritdoc />
    public partial class Productchangeidentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "Products",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 11, 20, 18, 29, 9, 11, DateTimeKind.Local).AddTicks(1541),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 11, 20, 17, 46, 8, 926, DateTimeKind.Local).AddTicks(575));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Consumables_ExpirationDate",
                table: "Products",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2028, 11, 20, 18, 29, 9, 11, DateTimeKind.Local).AddTicks(561),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2028, 11, 20, 17, 46, 8, 925, DateTimeKind.Local).AddTicks(9501));

            migrationBuilder.AlterColumn<long>(
                name: "UPC",
                table: "Products",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "UPC",
                keyValue: 1234567890L,
                column: "Consumables_ExpirationDate",
                value: new DateTime(2028, 11, 20, 18, 29, 9, 11, DateTimeKind.Local).AddTicks(561));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "Products",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 11, 20, 17, 46, 8, 926, DateTimeKind.Local).AddTicks(575),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 11, 20, 18, 29, 9, 11, DateTimeKind.Local).AddTicks(1541));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Consumables_ExpirationDate",
                table: "Products",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2028, 11, 20, 17, 46, 8, 925, DateTimeKind.Local).AddTicks(9501),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2028, 11, 20, 18, 29, 9, 11, DateTimeKind.Local).AddTicks(561));

            migrationBuilder.AlterColumn<long>(
                name: "UPC",
                table: "Products",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "UPC",
                keyValue: 1234567890L,
                column: "Consumables_ExpirationDate",
                value: new DateTime(2028, 11, 20, 17, 46, 8, 925, DateTimeKind.Local).AddTicks(9501));
        }
    }
}
