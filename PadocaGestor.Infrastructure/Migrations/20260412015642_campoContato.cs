using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PadocaGestor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class campoContato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Contato",
                table: "fornecedores",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contato",
                table: "fornecedores");
        }
    }
}
