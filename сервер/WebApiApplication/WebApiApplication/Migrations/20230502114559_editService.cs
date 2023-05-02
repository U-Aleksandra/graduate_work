using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiApplication.Migrations
{
    /// <inheritdoc />
    public partial class editService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Break",
                table: "Services",
                newName: "BreakTime");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionService",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionService",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "BreakTime",
                table: "Services",
                newName: "Break");
        }
    }
}
