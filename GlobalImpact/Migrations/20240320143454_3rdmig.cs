using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GlobalImpact.Migrations
{
    /// <inheritdoc />
    public partial class _3rdmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "76df34e4-9156-436a-b925-464311618545", "987d021e-c20d-4785-b2dd-941638f4c127", "client", "CLIENT" },
                    { "8b12135b-0f6e-49ce-a93a-aabad36298b0", "92651bdd-4f5f-4e19-8641-3f50d4b3789a", "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NIF", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Points", "SecurityStamp", "TwoFactorEnabled", "UniqueCode", "UserName" },
                values: new object[] { "75e0a63d-82ab-4de1-90d3-2575a681c8e0", 0, 0, "bed5e6dd-9544-45b5-b8e3-10174bacbb49", "admin@gmail.com", true, "Admin", "Admin", true, null, 0, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAECWVMTFTTZksDCbyxY991mV5C0Z/ihjrDPX7XbpNaYwfgZSVmA7Iovq+1Z5QeEtoSA==", "123456789", true, 0, "", false, "92303b34-a97b-4390-bcc5-e1b3c24181cb", "admin" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "Price", "ProductCategoryId", "Stock", "Tax" },
                values: new object[,]
                {
                    { new Guid("6f6f5a4f-d76a-4ff3-b508-8a8f97580417"), "Broculos Verde", "broculos.jpg", "Broculos", 1.5, "8da03371-2293-4884-b6c7-713c7298c23b", 50, 0.059999999999999998 },
                    { new Guid("725e4526-fe98-4a1b-9f8d-57f8e14a9cdc"), "Licor Beirao versao Especial 100 anos", "licro-beirao.jpg", "Licor Beirao", 11.199999999999999, "3c68860f-bbd7-4148-813e-b286d4f5cdac", 5, 0.23000000000000001 },
                    { new Guid("73e6d370-2be5-4f6e-bd62-533487617f27"), "Pessego da Colombia", "pessego.jpg", "Pessego", 2.2999999999999998, "5da577c4-2541-411c-821b-d872360aa686", 30, 0.059999999999999998 },
                    { new Guid("b3d43dc1-f7e9-45f1-83ef-d64b8fc083ca"), "Costoletas de Vaca", "Talho-Castro-Costeleta-Porco.jpg", "Costoletas", 5.0, "39b678c4-dc70-4b6b-b480-7d69f21041d9", 20, 0.059999999999999998 },
                    { new Guid("ded5bce1-5392-4ac1-9a3f-e7d2e0fc4467"), "Bacalhau da Noruega", "bacalhau.jpg", "Bacalhau", 8.0, "d6062833-7be0-44b2-997c-72aad44b470b", 25, 0.059999999999999998 }
                });

            migrationBuilder.InsertData(
                table: "ProductsCategory",
                columns: new[] { "ProductCategoryId", "Category" },
                values: new object[,]
                {
                    { new Guid("39b678c4-dc70-4b6b-b480-7d69f21041d9"), "talho" },
                    { new Guid("3c68860f-bbd7-4148-813e-b286d4f5cdac"), "bebidas" },
                    { new Guid("5da577c4-2541-411c-821b-d872360aa686"), "frutas" },
                    { new Guid("8da03371-2293-4884-b6c7-713c7298c23b"), "legumes" },
                    { new Guid("d6062833-7be0-44b2-997c-72aad44b470b"), "peixaria" }
                });

            migrationBuilder.InsertData(
                table: "RecyclingBinType",
                columns: new[] { "RecyclingBinTypeId", "Type" },
                values: new object[,]
                {
                    { new Guid("288e9bfa-96a7-4203-a3e5-731f44f9f9ee"), "paper" },
                    { new Guid("4c0787c3-0425-4e97-9ef8-950a52a8bc43"), "plastic" },
                    { new Guid("d46d87f5-cdfd-411c-a011-79c050c70a5b"), "glass" }
                });

            migrationBuilder.InsertData(
                table: "RecyclingBins",
                columns: new[] { "Id", "Capacity", "CurrentCapacity", "Description", "Latitude", "Longitude", "RecyclingBinTypeId", "Status" },
                values: new object[,]
                {
                    { new Guid("724b4db8-f38e-4903-8bb4-2d2a658eaf5e"), 100.0, 0.0, "Recycling Bin Glass", 38.519799793743871, -8.8360971667515606, "d46d87f5-cdfd-411c-a011-79c050c70a5b", true },
                    { new Guid("919fc860-f3ed-4ad2-8621-747ddb457def"), 100.0, 0.0, "Recycling Bin Glass", 38.521607817359822, -8.8368159603671987, "d46d87f5-cdfd-411c-a011-79c050c70a5b", true },
                    { new Guid("b5c9a27f-482d-47d5-a4fe-6b9e31b24edb"), 100.0, 0.0, "Recycling Bin Paper", 38.521474614438482, -8.8366557205732299, "288e9bfa-96a7-4203-a3e5-731f44f9f9ee", true },
                    { new Guid("d9b83fcb-38af-47dc-a7f1-30c01a257920"), 100.0, 0.0, "Recycling Bin Plastic", 38.522550713957862, -8.8395605732421387, "4c0787c3-0425-4e97-9ef8-950a52a8bc43", true },
                    { new Guid("e57f5882-457d-477a-b46b-4042b8efdfab"), 100.0, 0.0, "Recycling Bin Plastic", 38.52171490188254, -8.83694281687076, "4c0787c3-0425-4e97-9ef8-950a52a8bc43", true },
                    { new Guid("f7053e80-f7e3-4eaa-907a-0568a4937e53"), 100.0, 0.0, "Recycling Bin Paper", 38.522682016378347, -8.8397580181150541, "288e9bfa-96a7-4203-a3e5-731f44f9f9ee", true }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8b12135b-0f6e-49ce-a93a-aabad36298b0", "75e0a63d-82ab-4de1-90d3-2575a681c8e0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "76df34e4-9156-436a-b925-464311618545");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8b12135b-0f6e-49ce-a93a-aabad36298b0", "75e0a63d-82ab-4de1-90d3-2575a681c8e0" });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6f6f5a4f-d76a-4ff3-b508-8a8f97580417"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("725e4526-fe98-4a1b-9f8d-57f8e14a9cdc"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("73e6d370-2be5-4f6e-bd62-533487617f27"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b3d43dc1-f7e9-45f1-83ef-d64b8fc083ca"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ded5bce1-5392-4ac1-9a3f-e7d2e0fc4467"));

            migrationBuilder.DeleteData(
                table: "ProductsCategory",
                keyColumn: "ProductCategoryId",
                keyValue: new Guid("39b678c4-dc70-4b6b-b480-7d69f21041d9"));

            migrationBuilder.DeleteData(
                table: "ProductsCategory",
                keyColumn: "ProductCategoryId",
                keyValue: new Guid("3c68860f-bbd7-4148-813e-b286d4f5cdac"));

            migrationBuilder.DeleteData(
                table: "ProductsCategory",
                keyColumn: "ProductCategoryId",
                keyValue: new Guid("5da577c4-2541-411c-821b-d872360aa686"));

            migrationBuilder.DeleteData(
                table: "ProductsCategory",
                keyColumn: "ProductCategoryId",
                keyValue: new Guid("8da03371-2293-4884-b6c7-713c7298c23b"));

            migrationBuilder.DeleteData(
                table: "ProductsCategory",
                keyColumn: "ProductCategoryId",
                keyValue: new Guid("d6062833-7be0-44b2-997c-72aad44b470b"));

            migrationBuilder.DeleteData(
                table: "RecyclingBinType",
                keyColumn: "RecyclingBinTypeId",
                keyValue: new Guid("288e9bfa-96a7-4203-a3e5-731f44f9f9ee"));

            migrationBuilder.DeleteData(
                table: "RecyclingBinType",
                keyColumn: "RecyclingBinTypeId",
                keyValue: new Guid("4c0787c3-0425-4e97-9ef8-950a52a8bc43"));

            migrationBuilder.DeleteData(
                table: "RecyclingBinType",
                keyColumn: "RecyclingBinTypeId",
                keyValue: new Guid("d46d87f5-cdfd-411c-a011-79c050c70a5b"));

            migrationBuilder.DeleteData(
                table: "RecyclingBins",
                keyColumn: "Id",
                keyValue: new Guid("724b4db8-f38e-4903-8bb4-2d2a658eaf5e"));

            migrationBuilder.DeleteData(
                table: "RecyclingBins",
                keyColumn: "Id",
                keyValue: new Guid("919fc860-f3ed-4ad2-8621-747ddb457def"));

            migrationBuilder.DeleteData(
                table: "RecyclingBins",
                keyColumn: "Id",
                keyValue: new Guid("b5c9a27f-482d-47d5-a4fe-6b9e31b24edb"));

            migrationBuilder.DeleteData(
                table: "RecyclingBins",
                keyColumn: "Id",
                keyValue: new Guid("d9b83fcb-38af-47dc-a7f1-30c01a257920"));

            migrationBuilder.DeleteData(
                table: "RecyclingBins",
                keyColumn: "Id",
                keyValue: new Guid("e57f5882-457d-477a-b46b-4042b8efdfab"));

            migrationBuilder.DeleteData(
                table: "RecyclingBins",
                keyColumn: "Id",
                keyValue: new Guid("f7053e80-f7e3-4eaa-907a-0568a4937e53"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b12135b-0f6e-49ce-a93a-aabad36298b0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75e0a63d-82ab-4de1-90d3-2575a681c8e0");

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
    }
}
