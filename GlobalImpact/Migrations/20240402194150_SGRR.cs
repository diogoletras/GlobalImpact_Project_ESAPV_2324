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
                    { "862a3e4b-48f2-48b5-92a5-d633a4424185", "6d9713b0-8205-4629-a5ff-2c431f2ee28a", "admin", "ADMIN" },
                    { "bf60fa39-50ed-4f64-8e8a-0adf5dd5e9c7", "73a01f9f-a1ab-4cbd-8742-e064b2c8debe", "client", "CLIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NIF", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Points", "SecurityStamp", "TwoFactorEnabled", "UniqueCode", "UserName" },
                values: new object[,]
                {
                    { "08345297-5853-4b89-ad62-6a1b23c00dcc", 0, 0, "ee1d8506-8be4-40f5-8d03-bc549f5ac908", "admin@gmail.com", true, "Admin", "Admin", true, null, 0, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEM/dVKiYnhadx6Ev4VYV6MUn5OGL7008maXMXcesMEOrKpKQ4nUHjkZpQTPUFvUp1A==", "123456789", true, 10000, "", false, "f101563e-c9c7-4a7f-8bbc-066437d7344f", "admin" },
                    { "42003a85-23eb-4a34-bee0-66f05ab91e61", 0, 50, "1edcb778-69c4-46d6-9cbf-b47912c9ea92", "cliente@exemplo.com", true, "Cliente", "Cliente", true, null, 0, "CLIENTE@EXEMPLO.COM", "CLIENTE", "AQAAAAIAAYagAAAAEKt9B93zAq7ZTNd7VZe/jd5znuwkQfUNszASCHo8i9hq75I2aQRuFKuNbiE0OmxU5A==", "123456789", true, 10000, "", false, "cc5ec226-2e94-40f4-a0a2-036fae335413", "cliente" }
                });

            migrationBuilder.InsertData(
                table: "ProductTransactionStatus",
                columns: new[] { "ProductTransactionStatusId", "Status" },
                values: new object[,]
                {
                    { new Guid("1c065574-b70c-4c49-8949-01788d0cc0c2"), "Cancelled" },
                    { new Guid("6a29600e-1b3a-4726-9243-07c3685279fd"), "Pending" },
                    { new Guid("ec00cd92-5e26-473b-8060-0fb2f9853a68"), "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "Points", "ProductCategoryId", "Stock" },
                values: new object[,]
                {
                    { new Guid("0a656c7f-6a93-4d93-b0a6-709e6cc59435"), "Broculos Verde", "broculos.jpg", "Broculos", 1, "c46c467c-2c2a-406f-933a-d94e1bb1d708", 50 },
                    { new Guid("195f93f8-1169-4053-89ea-3af890734743"), "Bacalhau da Noruega", "bacalhau.jpg", "Bacalhau", 8, "896430dc-6dec-4bc6-a800-f57102bdde38", 25 },
                    { new Guid("28018811-e2ac-44e8-a87f-2da208e8be94"), "Pessego da Colombia", "pessego.jpg", "Pessego", 2, "bd25d7bf-2191-4cc9-85de-56dd53305046", 30 },
                    { new Guid("2b39fbec-4835-42ec-80df-24cf0e2d404d"), "Costoletas de Vaca", "Talho-Castro-Costeleta-Porco.jpg", "Costoletas", 5, "dfe993ee-e166-4b37-a7b0-3d1a79d73dcf", 20 },
                    { new Guid("2e521fb1-f2e2-4a9f-b632-23483ec58a31"), "Licor Beirao versao Especial 100 anos", "licro-beirao.jpg", "Licor Beirao", 11, "36c8176f-a513-4e8b-bdb4-5cf3af9f8e75", 5 }
                });

            migrationBuilder.InsertData(
                table: "ProductsCategory",
                columns: new[] { "ProductCategoryId", "Category" },
                values: new object[,]
                {
                    { new Guid("36c8176f-a513-4e8b-bdb4-5cf3af9f8e75"), "bebidas" },
                    { new Guid("896430dc-6dec-4bc6-a800-f57102bdde38"), "peixaria" },
                    { new Guid("bd25d7bf-2191-4cc9-85de-56dd53305046"), "frutas" },
                    { new Guid("c46c467c-2c2a-406f-933a-d94e1bb1d708"), "legumes" },
                    { new Guid("dfe993ee-e166-4b37-a7b0-3d1a79d73dcf"), "talho" }
                });

            migrationBuilder.InsertData(
                table: "RecyclingBinType",
                columns: new[] { "RecyclingBinTypeId", "Type" },
                values: new object[,]
                {
                    { new Guid("494e9f48-4598-45df-a7e4-91f2fe2d0055"), "paper" },
                    { new Guid("6b131442-388a-491f-9216-9da450a055bd"), "plastic" },
                    { new Guid("ca51ea2c-94df-46c1-8c34-95cc31e720fd"), "glass" }
                });

            migrationBuilder.InsertData(
                table: "RecyclingBins",
                columns: new[] { "Id", "Capacity", "CurrentCapacity", "Description", "Latitude", "Longitude", "RecyclingBinTypeId", "Status" },
                values: new object[,]
                {
                    { new Guid("0bc21829-4deb-4eca-9805-ea1df30a5a2f"), 100.0, 100.0, "Recycling Bin Paper", 38.522682016378347, -8.8397580181150541, "494e9f48-4598-45df-a7e4-91f2fe2d0055", true },
                    { new Guid("2e9e099c-a34a-40d6-8e53-fb0a2ab7cb3c"), 100.0, 0.0, "Recycling Bin Glass", 38.521607817359822, -8.8368159603671987, "ca51ea2c-94df-46c1-8c34-95cc31e720fd", false },
                    { new Guid("302268d0-5a63-4544-bfd6-658f043d6003"), 100.0, 100.0, "Recycling Bin Plastic", 38.522550713957862, -8.8395605732421387, "6b131442-388a-491f-9216-9da450a055bd", true },
                    { new Guid("5be8de11-4493-4c42-bcff-daf9f6bee6e1"), 100.0, 0.0, "Recycling Bin Glass", 38.519799793743871, -8.8360971667515606, "ca51ea2c-94df-46c1-8c34-95cc31e720fd", false },
                    { new Guid("9ce798ab-5d60-4229-8c65-df9d9c9ce54f"), 100.0, 0.0, "Recycling Bin Paper", 38.521474614438482, -8.8366557205732299, "494e9f48-4598-45df-a7e4-91f2fe2d0055", false },
                    { new Guid("bf4028b2-0729-4be7-981d-5047faf7bc4e"), 100.0, 0.0, "Recycling Bin Plastic", 38.52171490188254, -8.83694281687076, "6b131442-388a-491f-9216-9da450a055bd", false }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "862a3e4b-48f2-48b5-92a5-d633a4424185", "08345297-5853-4b89-ad62-6a1b23c00dcc" },
                    { "bf60fa39-50ed-4f64-8e8a-0adf5dd5e9c7", "42003a85-23eb-4a34-bee0-66f05ab91e61" }
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
