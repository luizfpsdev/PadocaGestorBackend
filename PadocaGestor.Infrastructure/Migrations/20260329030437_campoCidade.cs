using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PadocaGestor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class campoCidade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "fornecedores",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "fornecedores");
        }
    }
}
