using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PadocaGestor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class relacionamentoroleusuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_roles_usuario_id_roles",
                table: "roles_usuario",
                column: "id_roles");

            migrationBuilder.AddForeignKey(
                name: "FK_roles_usuario_roles_id_roles",
                table: "roles_usuario",
                column: "id_roles",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_roles_usuario_roles_id_roles",
                table: "roles_usuario");

            migrationBuilder.DropIndex(
                name: "IX_roles_usuario_id_roles",
                table: "roles_usuario");
        }
    }
}
