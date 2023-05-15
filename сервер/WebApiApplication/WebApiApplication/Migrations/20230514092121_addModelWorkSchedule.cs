using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiApplication.Migrations
{
    /// <inheritdoc />
    public partial class addModelWorkSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    StartWork = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndWork = table.Column<TimeSpan>(type: "time", nullable: false),
                    StartBreak = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndBreak = table.Column<TimeSpan>(type: "time", nullable: false),
                    SpecialistId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkSchedules_Users_SpecialistId",
                        column: x => x.SpecialistId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkSchedules_SpecialistId",
                table: "WorkSchedules",
                column: "SpecialistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkSchedules");
        }
    }
}
