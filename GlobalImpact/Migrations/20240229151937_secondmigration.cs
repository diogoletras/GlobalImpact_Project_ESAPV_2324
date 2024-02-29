using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GlobalImpact.Migrations
{
    /// <inheritdoc />
    public partial class secondmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "63bb5b27-7035-4218-abae-979aa5ca40f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eeaae30a-f87a-48de-b2ac-28561edcb577");

            migrationBuilder.DeleteData(
                table: "RecyclingBinType",
                keyColumn: "RecyclingBinTypeId",
                keyValue: new Guid("af2375b2-4d78-4bf9-8e6d-c225faf00b57"));

            migrationBuilder.DeleteData(
                table: "RecyclingBinType",
                keyColumn: "RecyclingBinTypeId",
                keyValue: new Guid("bfd99aad-fc45-4ce7-8cb8-ad21e3edc31a"));

            migrationBuilder.DeleteData(
                table: "RecyclingBinType",
                keyColumn: "RecyclingBinTypeId",
                keyValue: new Guid("d5b2b941-2f2a-4312-a634-dc8db345d503"));

            migrationBuilder.DropColumn(
                name: "isNIFRequired",
                table: "RecyclingTransactions");

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "RecyclingTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8d053eba-7dfd-4568-bb96-dd9670161803", "e31342ca-ae95-41e7-ac5f-ddd05cbf5591", "admin", "ADMIN" },
                    { "d5a5b851-f3cf-416f-a969-6d34d6b1c521", "9b52c9bc-c6da-4d4b-81b5-92b0b94b4e36", "client", "CLIENT" }
                });

            migrationBuilder.InsertData(
                table: "RecyclingBinType",
                columns: new[] { "RecyclingBinTypeId", "Type" },
                values: new object[,]
                {
                    { new Guid("715098c3-939f-4b93-9696-8a0bdb95d6e8"), "plastic" },
                    { new Guid("8c47cb3d-bc54-46c6-846e-febca18e92d5"), "glass" },
                    { new Guid("a6574a35-26ec-4978-8ac8-86b2b72dc397"), "paper" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d053eba-7dfd-4568-bb96-dd9670161803");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5a5b851-f3cf-416f-a969-6d34d6b1c521");

            migrationBuilder.DeleteData(
                table: "RecyclingBinType",
                keyColumn: "RecyclingBinTypeId",
                keyValue: new Guid("715098c3-939f-4b93-9696-8a0bdb95d6e8"));

            migrationBuilder.DeleteData(
                table: "RecyclingBinType",
                keyColumn: "RecyclingBinTypeId",
                keyValue: new Guid("8c47cb3d-bc54-46c6-846e-febca18e92d5"));

            migrationBuilder.DeleteData(
                table: "RecyclingBinType",
                keyColumn: "RecyclingBinTypeId",
                keyValue: new Guid("a6574a35-26ec-4978-8ac8-86b2b72dc397"));

            migrationBuilder.DropColumn(
                name: "Points",
                table: "RecyclingTransactions");

            migrationBuilder.AddColumn<bool>(
                name: "isNIFRequired",
                table: "RecyclingTransactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "63bb5b27-7035-4218-abae-979aa5ca40f6", "b5a3fb8d-a9e4-4144-84a1-c65fa1d96f9c", "client", "CLIENT" },
                    { "eeaae30a-f87a-48de-b2ac-28561edcb577", "9a70698a-6c85-4425-8078-d380dd4941b5", "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "RecyclingBinType",
                columns: new[] { "RecyclingBinTypeId", "Type" },
                values: new object[,]
                {
                    { new Guid("af2375b2-4d78-4bf9-8e6d-c225faf00b57"), "paper" },
                    { new Guid("bfd99aad-fc45-4ce7-8cb8-ad21e3edc31a"), "glass" },
                    { new Guid("d5b2b941-2f2a-4312-a634-dc8db345d503"), "plastic" }
                });
        }
    }
}
