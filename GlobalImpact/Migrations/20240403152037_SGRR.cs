using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GlobalImpact.Migrations
{
    /// <inheritdoc />
    public partial class SGRR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UniqueCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    NIF = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductCategoryId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductsCategory",
                columns: table => new
                {
                    ProductCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsCategory", x => x.ProductCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "ProductTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTransactionStatus",
                columns: table => new
                {
                    ProductTransactionStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTransactionStatus", x => x.ProductTransactionStatusId);
                });

            migrationBuilder.CreateTable(
                name: "RecyclingBins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecyclingBinTypeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<double>(type: "float", nullable: false),
                    CurrentCapacity = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecyclingBins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecyclingBinType",
                columns: table => new
                {
                    RecyclingBinTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecyclingBinType", x => x.RecyclingBinTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecyclingTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecyclingBinId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecyclingTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecyclingTransactions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecyclingTransactions_RecyclingBins_RecyclingBinId",
                        column: x => x.RecyclingBinId,
                        principalTable: "RecyclingBins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2127ccd4-5708-4494-8636-b0446c574672", "d432090e-de1a-4b59-83fd-f409f898e451", "admin", "ADMIN" },
                    { "8b57dd0c-cf88-4cdf-8473-9fc192d39172", "7bf444b2-3088-4867-b5da-c091d01ee1dd", "client", "CLIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NIF", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Points", "SecurityStamp", "TwoFactorEnabled", "UniqueCode", "UserName" },
                values: new object[,]
                {
                    { "2e776102-30bd-4681-8641-6864677b4fb6", 0, 0, "9a7ae475-fbbf-42c5-9f4b-85de9a72951c", "admin@gmail.com", true, "Admin", "Admin", true, null, 0, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEOWSkaNS9KIBCGupAvuycchOPTohY8SIJVl9P8/hGpN1uMSUrhelq1Z74MBeMcsH1w==", "123456789", true, 10000, "", false, "ebf6cdab-4ad2-42fb-9e9d-a24bc0675ab4", "admin" },
                    { "9f36305c-f344-4c4f-8099-44db5e3b7017", 0, 50, "9493ab7e-b8fa-40b6-97e4-e028b4775318", "cliente@exemplo.com", true, "Cliente", "Cliente", true, null, 0, "CLIENTE@EXEMPLO.COM", "CLIENTE", "AQAAAAIAAYagAAAAEPafaKHnrOR/mQi8uZjtSBzY2PO286mwX/PR0vnBUiUS+V9yahDNoaQ+cO3BJbMlyQ==", "123456789", true, 10000, "", false, "5cbb3168-81b2-41fc-995c-c8193beb2773", "cliente" }
                });

            migrationBuilder.InsertData(
                table: "ProductTransactionStatus",
                columns: new[] { "ProductTransactionStatusId", "Status" },
                values: new object[,]
                {
                    { new Guid("4b72727c-c20a-475a-86ec-3ab00470456d"), "Completed" },
                    { new Guid("a8640ee8-2323-47cd-a51c-04019ec795a2"), "Cancelled" },
                    { new Guid("ea7b8cc7-5732-4ed5-96cd-75334fce8c65"), "Pending" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "Points", "ProductCategoryId", "Stock" },
                values: new object[,]
                {
                    { new Guid("41822c68-14ce-49c5-813e-c8100222c22d"), "Licor Beirao versao Especial 100 anos", "licro-beirao.jpg", "Licor Beirao", 11, "f8fba2c5-10d1-4da8-b021-49cd96984ef3", 5 },
                    { new Guid("b33bc8ed-d9f5-4e2e-8fce-97588487e365"), "Costoletas de Vaca", "Talho-Castro-Costeleta-Porco.jpg", "Costoletas", 5, "887ca7ef-7ee5-47c3-a565-07a05f858590", 20 },
                    { new Guid("b87fe79f-829c-4c67-a924-d14d00750410"), "Bacalhau da Noruega", "bacalhau.jpg", "Bacalhau", 8, "321c58d1-5964-46ef-8ca1-752fbda5c9f7", 25 },
                    { new Guid("c32966c1-aa49-4551-97e6-58d0d7efd29c"), "Broculos Verde", "broculos.jpg", "Broculos", 1, "4890d39c-462a-4cd0-86dd-21bd0e5dfe93", 50 },
                    { new Guid("ead7178f-83ae-4355-bb34-cb566afea830"), "Pessego da Colombia", "pessego.jpg", "Pessego", 2, "b0add186-5ff0-4e85-ab1c-6e5ebeb2e989", 30 }
                });

            migrationBuilder.InsertData(
                table: "ProductsCategory",
                columns: new[] { "ProductCategoryId", "Category" },
                values: new object[,]
                {
                    { new Guid("321c58d1-5964-46ef-8ca1-752fbda5c9f7"), "peixaria" },
                    { new Guid("4890d39c-462a-4cd0-86dd-21bd0e5dfe93"), "legumes" },
                    { new Guid("887ca7ef-7ee5-47c3-a565-07a05f858590"), "talho" },
                    { new Guid("b0add186-5ff0-4e85-ab1c-6e5ebeb2e989"), "frutas" },
                    { new Guid("f8fba2c5-10d1-4da8-b021-49cd96984ef3"), "bebidas" }
                });

            migrationBuilder.InsertData(
                table: "RecyclingBinType",
                columns: new[] { "RecyclingBinTypeId", "Type" },
                values: new object[,]
                {
                    { new Guid("6f19a179-f17f-4e4b-b5b8-d81408b3acfb"), "paper" },
                    { new Guid("d84aead2-e9ab-43b6-af86-989c83ca7f9a"), "glass" },
                    { new Guid("e3a80ab0-840d-400a-bbe1-c55665d03fb9"), "plastic" }
                });

            migrationBuilder.InsertData(
                table: "RecyclingBins",
                columns: new[] { "Id", "Capacity", "CurrentCapacity", "Description", "Latitude", "Longitude", "RecyclingBinTypeId", "Status" },
                values: new object[,]
                {
                    { new Guid("0b7192b7-8d8c-459c-a760-f77c63065d8a"), 100.0, 0.0, "Recycling Bin Glass", 38.519799793743871, -8.8360971667515606, "d84aead2-e9ab-43b6-af86-989c83ca7f9a", false },
                    { new Guid("5fbfe4c8-d979-4624-8c7c-a5935b718c94"), 100.0, 100.0, "Recycling Bin Plastic", 38.522550713957862, -8.8395605732421387, "e3a80ab0-840d-400a-bbe1-c55665d03fb9", true },
                    { new Guid("6f771c1b-ca28-4af3-9091-a232d7d5bb69"), 100.0, 0.0, "Recycling Bin Plastic", 38.52171490188254, -8.83694281687076, "e3a80ab0-840d-400a-bbe1-c55665d03fb9", false },
                    { new Guid("c1f46bf8-5f7c-4314-afe4-c74fd6f67d89"), 100.0, 100.0, "Recycling Bin Paper", 38.522682016378347, -8.8397580181150541, "6f19a179-f17f-4e4b-b5b8-d81408b3acfb", true },
                    { new Guid("e9050ec1-ea8f-46ab-a982-673284e22c51"), 100.0, 0.0, "Recycling Bin Glass", 38.521607817359822, -8.8368159603671987, "d84aead2-e9ab-43b6-af86-989c83ca7f9a", false },
                    { new Guid("eb1c6a70-517d-4d6d-8e2b-3ace6b7131d2"), 100.0, 0.0, "Recycling Bin Paper", 38.521474614438482, -8.8366557205732299, "6f19a179-f17f-4e4b-b5b8-d81408b3acfb", false }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2127ccd4-5708-4494-8636-b0446c574672", "2e776102-30bd-4681-8641-6864677b4fb6" },
                    { "8b57dd0c-cf88-4cdf-8473-9fc192d39172", "9f36305c-f344-4c4f-8099-44db5e3b7017" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RecyclingTransactions_RecyclingBinId",
                table: "RecyclingTransactions",
                column: "RecyclingBinId");

            migrationBuilder.CreateIndex(
                name: "IX_RecyclingTransactions_UserId",
                table: "RecyclingTransactions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductsCategory");

            migrationBuilder.DropTable(
                name: "ProductTransactions");

            migrationBuilder.DropTable(
                name: "ProductTransactionStatus");

            migrationBuilder.DropTable(
                name: "RecyclingBinType");

            migrationBuilder.DropTable(
                name: "RecyclingTransactions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "RecyclingBins");
        }
    }
}
