using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderNumber",
                table: "Pedidos",
                newName: "Order");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Pedidos",
                newName: "Client");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Order",
                table: "Pedidos",
                newName: "OrderNumber");

            migrationBuilder.RenameColumn(
                name: "Client",
                table: "Pedidos",
                newName: "ClientId");
        }
    }
}
