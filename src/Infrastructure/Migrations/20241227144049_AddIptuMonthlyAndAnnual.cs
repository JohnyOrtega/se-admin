using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIptuMonthlyAndAnnual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IptuValue",
                table: "Imoveis",
                newName: "IptuMonthly");

            migrationBuilder.AddColumn<decimal>(
                name: "IptuAnnual",
                table: "Imoveis",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IptuAnnual",
                table: "Imoveis");

            migrationBuilder.RenameColumn(
                name: "IptuMonthly",
                table: "Imoveis",
                newName: "IptuValue");
        }
    }
}
