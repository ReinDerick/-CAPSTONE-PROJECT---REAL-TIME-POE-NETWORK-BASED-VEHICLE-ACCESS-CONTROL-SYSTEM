using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BsuAdmin.Migrations
{
    /// <inheritdoc />
    public partial class PlateNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recentlyNewPlates");

            migrationBuilder.AddColumn<bool>(
                name: "isRegistered",
                table: "NewPlates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isRegistered",
                table: "NewPlates");

            migrationBuilder.CreateTable(
                name: "recentlyNewPlates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recentlyNewPlates", x => x.Id);
                });
        }
    }
}
