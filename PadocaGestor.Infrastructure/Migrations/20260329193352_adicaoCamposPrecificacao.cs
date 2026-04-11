using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PadocaGestor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adicaoCamposPrecificacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Markup",
                table: "receitas",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecoVenda",
                table: "receitas",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TipoPrecificacao",
                table: "receitas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "fornecedores",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Markup",
                table: "receitas");

            migrationBuilder.DropColumn(
                name: "PrecoVenda",
                table: "receitas");

            migrationBuilder.DropColumn(
                name: "TipoPrecificacao",
                table: "receitas");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "fornecedores");
        }
    }
}
