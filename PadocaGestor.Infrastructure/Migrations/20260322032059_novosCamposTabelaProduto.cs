using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PadocaGestor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class novosCamposTabelaProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "produto",
                newName: "nome");

            migrationBuilder.AddColumn<decimal>(
                name: "markup",
                table: "produto",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "preco",
                table: "produto",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "tipo_precificacao",
                table: "produto",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "markup",
                table: "produto");

            migrationBuilder.DropColumn(
                name: "preco",
                table: "produto");

            migrationBuilder.DropColumn(
                name: "tipo_precificacao",
                table: "produto");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "produto",
                newName: "Nome");
        }
    }
}
