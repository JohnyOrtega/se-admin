using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NeighboorhoudAndCEPProprietario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Neighboor",
                table: "Proprietarios",
                newName: "Neighboorhoud");

            migrationBuilder.AddColumn<string>(
                name: "CEP",
                table: "Proprietarios",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CEP",
                table: "Proprietarios");

            migrationBuilder.RenameColumn(
                name: "Neighboorhoud",
                table: "Proprietarios",
                newName: "Neighboor");
        }
    }
}
