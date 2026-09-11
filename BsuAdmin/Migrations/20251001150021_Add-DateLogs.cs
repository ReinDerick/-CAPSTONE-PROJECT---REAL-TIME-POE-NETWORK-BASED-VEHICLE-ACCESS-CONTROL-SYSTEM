using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BsuAdmin.Migrations
{
    /// <inheritdoc />
    public partial class AddDateLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateLogs",
                table: "checkPlates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateLogs",
                table: "checkPlates");
        }
    }
}
