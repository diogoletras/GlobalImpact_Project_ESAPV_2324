using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GlobalImpact.Migrations
{
    /// <inheritdoc />
    public partial class _2ndCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "db98dbcc-8b7e-41e1-8200-2bc6eeb0a6d2");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0349cbaa-0b52-4169-a7c7-392d4dd269c8", "6ff07a90-9892-4172-a1a1-bffd0ffe2761" });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4c8316c2-aa79-4238-b6c0-6feceaabd510"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("889a9739-b0a6-459a-ac1e-9affdefd1532"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bde1eea7-4ae6-4dd4-8833-740a0649bd5a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e291f06d-5ef9-4cc6-aa6d-694662d99afb"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f647ce91-a7e7-40cb-98c0-b21b5808bb26"));

            migrationBuilder.DeleteData(
                table: "ProductsCategory",
                keyColumn: "ProductCategoryId",
                keyValue: new Guid("0e2aa289-b368-46f4-b376-decb8e18e7d0"));

            migrationBuilder.DeleteData(
                table: "ProductsCategory",
                keyColumn: "ProductCategoryId",
                keyValue: new Guid("77830269-7341-49fc-9e7f-e96ff9f45117"));

            migrationBuilder.DeleteData(
                table: "ProductsCategory",
                keyColumn: "ProductCategoryId",
                keyValue: new Guid("77d73e82-d9c9-4ed1-825f-52c167d97767"));

            migrationBuilder.DeleteData(
                table: "ProductsCategory",
                keyColumn: "ProductCategoryId",
                keyValue: new Guid("ca34bec2-01b9-44f7-9502-dee2248a1ae0"));

            migrationBuilder.DeleteData(
                table: "ProductsCategory",
                keyColumn: "ProductCategoryId",
                keyValue: new Guid("e5b02325-1365-4e2d-8ce0-a57759ef1164"));

            migrationBuilder.DeleteData(
                table: "RecyclingBinType",
                keyColumn: "RecyclingBinTypeId",
                keyValue: new Guid("5cf99d04-0d70-4902-bc53-59940a38a716"));

            migrationBuilder.DeleteData(
                table: "RecyclingBinType",
                keyColumn: "RecyclingBinTypeId",
                keyValue: new Guid("63b362f3-b244-4591-a939-d4788851cd22"));

            migrationBuilder.DeleteData(
                table: "RecyclingBinType",
                keyColumn: "RecyclingBinTypeId",
                keyValue: new Guid("bab57348-c74f-4144-ace9-8e0518af60f7"));

            migrationBuilder.DeleteData(
                table: "RecyclingBins",
                keyColumn: "Id",
                keyValue: new Guid("0076cdfd-28bb-4015-8a5b-0d3fe384f1c8"));

            migrationBuilder.DeleteData(
                table: "RecyclingBins",
                keyColumn: "Id",
                keyValue: new Guid("4b416cc5-6557-427a-8ae1-8f83cf856d0d"));

            migrationBuilder.DeleteData(
                table: "RecyclingBins",
                keyColumn: "Id",
                keyValue: new Guid("6899b3da-1bb3-4c09-badc-b40f0331d492"));

            migrationBuilder.DeleteData(
                table: "RecyclingBins",
                keyColumn: "Id",
                keyValue: new Guid("76053444-9535-4682-9cae-b37ae43f6639"));

            migrationBuilder.DeleteData(
                table: "RecyclingBins",
                keyColumn: "Id",
                keyValue: new Guid("8efd4fcc-e265-4faf-9b9e-5a498c4ebcbf"));

            migrationBuilder.DeleteData(
                table: "RecyclingBins",
                keyColumn: "Id",
                keyValue: new Guid("9857871a-035d-4edb-8e2a-a4ec5b8f60c5"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0349cbaa-0b52-4169-a7c7-392d4dd269c8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ff07a90-9892-4172-a1a1-bffd0ffe2761");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ca022bc6-42a8-45a4-add6-b7964bd9e3c3", "4fba6f66-3e75-4d78-b7ce-23acc6550e59", "client", "CLIENT" },
                    { "f2e90b8a-e637-4631-b651-946c71b657c1", "5fa359af-6cb1-4e32-9850-d1c2d0c01f3d", "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NIF", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Points", "SecurityStamp", "TwoFactorEnabled", "UniqueCode", "UserName" },
                values: new object[] { "802cbe4e-e8a7-4660-9aed-2b8c7b422a2d", 0, 0, "d112a32c-7e35-4be6-b738-c85755422325", "admin@gmail.com", true, "Admin", "Admin", true, null, 0, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAELtYdQteEL0AlT1JtOxRHC/4mUJAtErjbqL5co8RAtOiTiuHgjt9NDPWaC6V+yDOjg==", "123456789", true, 0, "", false, "eb6b2dc4-1ae4-4b72-95ea-fd41c556c9a8", "admin" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "Price", "ProductCategoryId", "Stock", "Tax" },
                values: new object[,]
                {
                    { new Guid("1135a783-4f2f-4651-9b17-5f817d4c8b7c"), "Pessego da Colombia", "", "Pessego", 2.2999999999999998, "91f5d8ba-899b-447d-9f2c-aa66351e5894", 30, 0.059999999999999998 },
                    { new Guid("1304f9ca-5d92-4b12-bde7-60e40ec09ffa"), "Broculos Verde", "", "Broculos", 1.5, "059fd93b-8158-412d-bda0-60d5671b7ab1", 50, 0.059999999999999998 },
                    { new Guid("20744865-a7ad-4104-9dfd-9ce7c01c68a9"), "Bacalhau da Noruega", "", "Bacalhau", 8.0, "636ca5aa-6afe-48e2-b568-acc5a0cfdd8b", 25, 0.059999999999999998 },
                    { new Guid("2673dd79-b4e9-4b15-bbae-e6ee7124c192"), "Licor Beirao versao Especial 100 anos", "", "Licor Beirao", 11.199999999999999, "4a0b9410-9810-400a-b704-7360181a8084", 5, 0.23000000000000001 },
                    { new Guid("394a8d2b-7507-4de5-a4f3-13ac8646ea1a"), "Costoletas de Vaca", "", "Costoletas", 5.0, "eed14674-4728-40d2-a30a-a58864d22e3a", 20, 0.059999999999999998 }
                });

            migrationBuilder.InsertData(
                table: "ProductsCategory",
                columns: new[] { "ProductCategoryId", "Category" },
                values: new object[,]
                {
                    { new Guid("059fd93b-8158-412d-bda0-60d5671b7ab1"), "legumes" },
                    { new Guid("4a0b9410-9810-400a-b704-7360181a8084"), "bebidas" },
                    { new Guid("636ca5aa-6afe-48e2-b568-acc5a0cfdd8b"), "peixaria" },
                    { new Guid("91f5d8ba-899b-447d-9f2c-aa66351e5894"), "frutas" },
                    { new Guid("eed14674-4728-40d2-a30a-a58864d22e3a"), "talho" }
                });

            migrationBuilder.InsertData(
                table: "RecyclingBinType",
                columns: new[] { "RecyclingBinTypeId", "Type" },
                values: new object[,]
                {
                    { new Guid("82ec2f6e-945c-4d05-95b9-fa9976aa99d8"), "paper" },
                    { new Guid("b477c4a0-cb7e-403f-ba69-ef1829231947"), "glass" },
                    { new Guid("ea187030-f460-48d5-a88d-55dd0b205cbe"), "plastic" }
                });

            migrationBuilder.InsertData(
                table: "RecyclingBins",
                columns: new[] { "Id", "Capacity", "CurrentCapacity", "Description", "Latitude", "Longitude", "RecyclingBinTypeId", "Status" },
                values: new object[,]
                {
                    { new Guid("0fa9e1e3-17b5-4bde-a30a-78b57402e21c"), 100.0, 0.0, "Recycling Bin Glass", 38.521607817359822, -8.8368159603671987, "b477c4a0-cb7e-403f-ba69-ef1829231947", true },
                    { new Guid("27a0a542-92ce-459b-a0ff-60fd9e09d8ed"), 100.0, 0.0, "Recycling Bin Paper", 38.522682016378347, -8.8397580181150541, "82ec2f6e-945c-4d05-95b9-fa9976aa99d8", true },
                    { new Guid("3aec152d-6c47-422d-b207-ddcfe8cbe5c5"), 100.0, 0.0, "Recycling Bin Plastic", 38.52171490188254, -8.83694281687076, "ea187030-f460-48d5-a88d-55dd0b205cbe", true },
                    { new Guid("4f686b11-a48c-4bde-875a-93591f5bcf8b"), 100.0, 0.0, "Recycling Bin Paper", 38.521474614438482, -8.8366557205732299, "82ec2f6e-945c-4d05-95b9-fa9976aa99d8", true },
                    { new Guid("66cd4a88-7c8b-436e-9903-9616dd038dba"), 100.0, 0.0, "Recycling Bin Plastic", 38.522550713957862, -8.8395605732421387, "ea187030-f460-48d5-a88d-55dd0b205cbe", true },
                    { new Guid("758e61ce-cbfd-4611-a530-3be355e706eb"), 100.0, 0.0, "Recycling Bin Glass", 38.519799793743871, -8.8360971667515606, "b477c4a0-cb7e-403f-ba69-ef1829231947", true }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f2e90b8a-e637-4631-b651-946c71b657c1", "802cbe4e-e8a7-4660-9aed-2b8c7b422a2d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca022bc6-42a8-45a4-add6-b7964bd9e3c3");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f2e90b8a-e637-4631-b651-946c71b657c1", "802cbe4e-e8a7-4660-9aed-2b8c7b422a2d" });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1135a783-4f2f-4651-9b17-5f817d4c8b7c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1304f9ca-5d92-4b12-bde7-60e40ec09ffa"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20744865-a7ad-4104-9dfd-9ce7c01c68a9"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2673dd79-b4e9-4b15-bbae-e6ee7124c192"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("394a8d2b-7507-4de5-a4f3-13ac8646ea1a"));

            migrationBuilder.DeleteData(
                table: "ProductsCategory",
                keyColumn: "ProductCategoryId",
                keyValue: new Guid("059fd93b-8158-412d-bda0-60d5671b7ab1"));

            migrationBuilder.DeleteData(
                table: "ProductsCategory",
                keyColumn: "ProductCategoryId",
                keyValue: new Guid("4a0b9410-9810-400a-b704-7360181a8084"));

            migrationBuilder.DeleteData(
                table: "ProductsCategory",
                keyColumn: "ProductCategoryId",
                keyValue: new Guid("636ca5aa-6afe-48e2-b568-acc5a0cfdd8b"));

            migrationBuilder.DeleteData(
                table: "ProductsCategory",
                keyColumn: "ProductCategoryId",
                keyValue: new Guid("91f5d8ba-899b-447d-9f2c-aa66351e5894"));

            migrationBuilder.DeleteData(
                table: "ProductsCategory",
                keyColumn: "ProductCategoryId",
                keyValue: new Guid("eed14674-4728-40d2-a30a-a58864d22e3a"));

            migrationBuilder.DeleteData(
                table: "RecyclingBinType",
                keyColumn: "RecyclingBinTypeId",
                keyValue: new Guid("82ec2f6e-945c-4d05-95b9-fa9976aa99d8"));

            migrationBuilder.DeleteData(
                table: "RecyclingBinType",
                keyColumn: "RecyclingBinTypeId",
                keyValue: new Guid("b477c4a0-cb7e-403f-ba69-ef1829231947"));

            migrationBuilder.DeleteData(
                table: "RecyclingBinType",
                keyColumn: "RecyclingBinTypeId",
                keyValue: new Guid("ea187030-f460-48d5-a88d-55dd0b205cbe"));

            migrationBuilder.DeleteData(
                table: "RecyclingBins",
                keyColumn: "Id",
                keyValue: new Guid("0fa9e1e3-17b5-4bde-a30a-78b57402e21c"));

            migrationBuilder.DeleteData(
                table: "RecyclingBins",
                keyColumn: "Id",
                keyValue: new Guid("27a0a542-92ce-459b-a0ff-60fd9e09d8ed"));

            migrationBuilder.DeleteData(
                table: "RecyclingBins",
                keyColumn: "Id",
                keyValue: new Guid("3aec152d-6c47-422d-b207-ddcfe8cbe5c5"));

            migrationBuilder.DeleteData(
                table: "RecyclingBins",
                keyColumn: "Id",
                keyValue: new Guid("4f686b11-a48c-4bde-875a-93591f5bcf8b"));

            migrationBuilder.DeleteData(
                table: "RecyclingBins",
                keyColumn: "Id",
                keyValue: new Guid("66cd4a88-7c8b-436e-9903-9616dd038dba"));

            migrationBuilder.DeleteData(
                table: "RecyclingBins",
                keyColumn: "Id",
                keyValue: new Guid("758e61ce-cbfd-4611-a530-3be355e706eb"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f2e90b8a-e637-4631-b651-946c71b657c1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "802cbe4e-e8a7-4660-9aed-2b8c7b422a2d");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Products");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0349cbaa-0b52-4169-a7c7-392d4dd269c8", "675cd7f9-67ca-49d9-a546-8d700aa28398", "admin", "ADMIN" },
                    { "db98dbcc-8b7e-41e1-8200-2bc6eeb0a6d2", "a9c97537-cad2-4b33-9803-bde9d43cfeb6", "client", "CLIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NIF", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Points", "SecurityStamp", "TwoFactorEnabled", "UniqueCode", "UserName" },
                values: new object[] { "6ff07a90-9892-4172-a1a1-bffd0ffe2761", 0, 0, "3df02139-4692-4039-b35e-a7ec468ceafa", "admin@gmail.com", true, "Admin", "Admin", true, null, 0, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAECzhoIzKygj5RDmu5aifLEoyRj/rLYOYQK1kQ/6uD5qYSCouJOHvk2Xig/JGO2Jgeg==", "123456789", true, 0, "", false, "d1f0b7cc-8db0-48a5-b185-edfc0e66f7c5", "admin" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price", "ProductCategoryId", "Stock", "Tax" },
                values: new object[,]
                {
                    { new Guid("4c8316c2-aa79-4238-b6c0-6feceaabd510"), "Broculos Verde", "Broculos", 1.5, "0e2aa289-b368-46f4-b376-decb8e18e7d0", 50, 0.059999999999999998 },
                    { new Guid("889a9739-b0a6-459a-ac1e-9affdefd1532"), "Bacalhau da Noruega", "Bacalhau", 8.0, "77d73e82-d9c9-4ed1-825f-52c167d97767", 25, 0.059999999999999998 },
                    { new Guid("bde1eea7-4ae6-4dd4-8833-740a0649bd5a"), "Licor Beirao versao Especial 100 anos", "Licor Beirao", 11.199999999999999, "77830269-7341-49fc-9e7f-e96ff9f45117", 5, 0.23000000000000001 },
                    { new Guid("e291f06d-5ef9-4cc6-aa6d-694662d99afb"), "Costoletas de Vaca", "Costoletas", 5.0, "ca34bec2-01b9-44f7-9502-dee2248a1ae0", 20, 0.059999999999999998 },
                    { new Guid("f647ce91-a7e7-40cb-98c0-b21b5808bb26"), "Pessego da Colombia", "Pessego", 2.2999999999999998, "e5b02325-1365-4e2d-8ce0-a57759ef1164", 30, 0.059999999999999998 }
                });

            migrationBuilder.InsertData(
                table: "ProductsCategory",
                columns: new[] { "ProductCategoryId", "Category" },
                values: new object[,]
                {
                    { new Guid("0e2aa289-b368-46f4-b376-decb8e18e7d0"), "legumes" },
                    { new Guid("77830269-7341-49fc-9e7f-e96ff9f45117"), "bebidas" },
                    { new Guid("77d73e82-d9c9-4ed1-825f-52c167d97767"), "peixaria" },
                    { new Guid("ca34bec2-01b9-44f7-9502-dee2248a1ae0"), "talho" },
                    { new Guid("e5b02325-1365-4e2d-8ce0-a57759ef1164"), "frutas" }
                });

            migrationBuilder.InsertData(
                table: "RecyclingBinType",
                columns: new[] { "RecyclingBinTypeId", "Type" },
                values: new object[,]
                {
                    { new Guid("5cf99d04-0d70-4902-bc53-59940a38a716"), "plastic" },
                    { new Guid("63b362f3-b244-4591-a939-d4788851cd22"), "paper" },
                    { new Guid("bab57348-c74f-4144-ace9-8e0518af60f7"), "glass" }
                });

            migrationBuilder.InsertData(
                table: "RecyclingBins",
                columns: new[] { "Id", "Capacity", "CurrentCapacity", "Description", "Latitude", "Longitude", "RecyclingBinTypeId", "Status" },
                values: new object[,]
                {
                    { new Guid("0076cdfd-28bb-4015-8a5b-0d3fe384f1c8"), 100.0, 0.0, "Recycling Bin Glass", 38.519799793743871, -8.8360971667515606, "bab57348-c74f-4144-ace9-8e0518af60f7", true },
                    { new Guid("4b416cc5-6557-427a-8ae1-8f83cf856d0d"), 100.0, 0.0, "Recycling Bin Glass", 38.521607817359822, -8.8368159603671987, "bab57348-c74f-4144-ace9-8e0518af60f7", true },
                    { new Guid("6899b3da-1bb3-4c09-badc-b40f0331d492"), 100.0, 0.0, "Recycling Bin Paper", 38.521474614438482, -8.8366557205732299, "63b362f3-b244-4591-a939-d4788851cd22", true },
                    { new Guid("76053444-9535-4682-9cae-b37ae43f6639"), 100.0, 0.0, "Recycling Bin Plastic", 38.52171490188254, -8.83694281687076, "5cf99d04-0d70-4902-bc53-59940a38a716", true },
                    { new Guid("8efd4fcc-e265-4faf-9b9e-5a498c4ebcbf"), 100.0, 0.0, "Recycling Bin Plastic", 38.522550713957862, -8.8395605732421387, "5cf99d04-0d70-4902-bc53-59940a38a716", true },
                    { new Guid("9857871a-035d-4edb-8e2a-a4ec5b8f60c5"), 100.0, 0.0, "Recycling Bin Paper", 38.522682016378347, -8.8397580181150541, "63b362f3-b244-4591-a939-d4788851cd22", true }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0349cbaa-0b52-4169-a7c7-392d4dd269c8", "6ff07a90-9892-4172-a1a1-bffd0ffe2761" });
        }
    }
}
