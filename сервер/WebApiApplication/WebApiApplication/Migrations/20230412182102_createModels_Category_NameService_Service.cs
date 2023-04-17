using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiApplication.Migrations
{
    /// <inheritdoc />
    public partial class createModelsCategoryNameServiceService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NameServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NameServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NameServices_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    StartPrice = table.Column<bool>(type: "bit", nullable: false),
                    ServicesTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Break = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NameServiceId = table.Column<int>(type: "int", nullable: true),
                    SpecialistId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_NameServices_NameServiceId",
                        column: x => x.NameServiceId,
                        principalTable: "NameServices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Services_Users_SpecialistId",
                        column: x => x.SpecialistId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NameServices_CategoryId",
                table: "NameServices",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_NameServiceId",
                table: "Services",
                column: "NameServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_SpecialistId",
                table: "Services",
                column: "SpecialistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "NameServices");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
