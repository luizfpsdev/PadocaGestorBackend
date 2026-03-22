using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PadocaGestor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class campoPreco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "preco",
                table: "produto_preco",
                type: "numeric(10,4)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "preco",
                table: "produto_preco");
        }
    }
}
