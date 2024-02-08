using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalImpact.Migrations
{
    /// <inheritdoc />
    public partial class Create3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReciclingBins_Location_LocationId",
                table: "ReciclingBins");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropIndex(
                name: "IX_ReciclingBins_LocationId",
                table: "ReciclingBins");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "ReciclingBins");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "ReciclingBins",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "ReciclingBins",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "ReciclingBins");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "ReciclingBins");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "ReciclingBins",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    latitude = table.Column<double>(type: "float", nullable: false),
                    longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReciclingBins_LocationId",
                table: "ReciclingBins",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReciclingBins_Location_LocationId",
                table: "ReciclingBins",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
