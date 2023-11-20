using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Migrations
{
    /// <inheritdoc />
    public partial class Userchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "Products",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 11, 20, 17, 46, 8, 926, DateTimeKind.Local).AddTicks(575),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 11, 20, 17, 43, 44, 959, DateTimeKind.Local).AddTicks(760));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Consumables_ExpirationDate",
                table: "Products",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2028, 11, 20, 17, 46, 8, 925, DateTimeKind.Local).AddTicks(9501),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2028, 11, 20, 17, 43, 44, 958, DateTimeKind.Local).AddTicks(9788));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "UPC",
                keyValue: 1234567890L,
                column: "Consumables_ExpirationDate",
                value: new DateTime(2028, 11, 20, 17, 46, 8, 925, DateTimeKind.Local).AddTicks(9501));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Email",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Email",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                column: "Email",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "Products",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 11, 20, 17, 43, 44, 959, DateTimeKind.Local).AddTicks(760),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 11, 20, 17, 46, 8, 926, DateTimeKind.Local).AddTicks(575));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Consumables_ExpirationDate",
                table: "Products",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2028, 11, 20, 17, 43, 44, 958, DateTimeKind.Local).AddTicks(9788),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2028, 11, 20, 17, 46, 8, 925, DateTimeKind.Local).AddTicks(9501));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "UPC",
                keyValue: 1234567890L,
                column: "Consumables_ExpirationDate",
                value: new DateTime(2028, 11, 20, 17, 43, 44, 958, DateTimeKind.Local).AddTicks(9788));
        }
    }
}
