using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BB.Finances.Data.Migrations
{
    /// <inheritdoc />
    public partial class AccountAndCategoryMarksForDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("12d28c54-8a7e-46d3-a9d7-8d33162f513c"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("547f38a7-35bb-4eb4-939c-b2107b7c0154"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("986a055b-949a-46a9-82e9-57f30c04027a"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("a19f84e1-2e62-4f8c-90ca-56a1292718b7"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "CurrencySign", "Name" },
                values: new object[,]
                {
                    { new Guid("52576b9f-614c-43e7-ade0-b4185a718edc"), "EUR", "EURO" },
                    { new Guid("8a5286ec-b73f-4e8f-ab60-435de89c7b52"), "RUB", "Russian rouble" },
                    { new Guid("c80aea7e-a8e0-46a6-b2a1-0f999eacc7bb"), "BYN", "Belarussian rouble" },
                    { new Guid("daee03af-4ec5-417c-a2ed-52f80a56b8ba"), "USD", "US Dollar" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("52576b9f-614c-43e7-ade0-b4185a718edc"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("8a5286ec-b73f-4e8f-ab60-435de89c7b52"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("c80aea7e-a8e0-46a6-b2a1-0f999eacc7bb"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("daee03af-4ec5-417c-a2ed-52f80a56b8ba"));

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Accounts");

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "CurrencySign", "Name" },
                values: new object[,]
                {
                    { new Guid("12d28c54-8a7e-46d3-a9d7-8d33162f513c"), "USD", "US Dollar" },
                    { new Guid("547f38a7-35bb-4eb4-939c-b2107b7c0154"), "EUR", "EURO" },
                    { new Guid("986a055b-949a-46a9-82e9-57f30c04027a"), "RUB", "Russian rouble" },
                    { new Guid("a19f84e1-2e62-4f8c-90ca-56a1292718b7"), "BYN", "Belarussian rouble" }
                });
        }
    }
}
