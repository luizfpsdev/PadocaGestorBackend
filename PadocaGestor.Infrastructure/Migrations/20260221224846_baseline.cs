using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PadocaGestor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class baseline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cliente_pk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "fornecedores",
                columns: table => new
                {
                    id_fornecedor = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    cnpj = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    ativo = table.Column<bool>(type: "boolean", nullable: true),
                    observacao = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    endereco = table.Column<string>(type: "character varying", nullable: true),
                    telefone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    client_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("fornecedores_pk", x => x.id_fornecedor);
                });

            migrationBuilder.CreateTable(
                name: "funcionarios",
                columns: table => new
                {
                    id_funcionario = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    telefone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    client_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("funcionarios_pk", x => x.id_funcionario);
                });

            migrationBuilder.CreateTable(
                name: "ingrediente",
                columns: table => new
                {
                    id_ingrediente = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    client_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ingrediente_pk", x => x.id_ingrediente);
                });

            migrationBuilder.CreateTable(
                name: "marca",
                columns: table => new
                {
                    id_marca = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    client_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("marca_pk", x => x.id_marca);
                });

            migrationBuilder.CreateTable(
                name: "receitas",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nome = table.Column<string>(type: "character varying", nullable: false),
                    preparo = table.Column<string>(type: "character varying", nullable: true),
                    data_criacao = table.Column<DateTime>(type: "timestamp", nullable: true),
                    id_empresa = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    rendimento = table.Column<decimal>(type: "numeric(10,4)", precision: 10, scale: 4, nullable: true),
                    client_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("receitas_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Role = table.Column<string>(type: "role", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    email = table.Column<string>(type: "email", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("usuario_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "produto",
                columns: table => new
                {
                    id_produto = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    id_ingrediente = table.Column<long>(type: "bigint", nullable: false),
                    descricao = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    id_marca = table.Column<long>(type: "bigint", nullable: true),
                    client_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("produto_pk", x => x.id_produto);
                    table.ForeignKey(
                        name: "id_ingrediente_foreign_key",
                        column: x => x.id_ingrediente,
                        principalTable: "ingrediente",
                        principalColumn: "id_ingrediente");
                    table.ForeignKey(
                        name: "id_marca_foreign_key",
                        column: x => x.id_marca,
                        principalTable: "marca",
                        principalColumn: "id_marca");
                });

            migrationBuilder.CreateTable(
                name: "receitas_versao",
                columns: table => new
                {
                    id_receita_versao = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    id_receitas = table.Column<long>(type: "bigint", nullable: true),
                    data_versao = table.Column<DateTime>(type: "timestamp", nullable: true),
                    alterado_por = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("receitas_versao_pk", x => x.id_receita_versao);
                    table.ForeignKey(
                        name: "receitas_fk",
                        column: x => x.id_receitas,
                        principalTable: "receitas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "roles_usuario",
                columns: table => new
                {
                    id_usuario = table.Column<string>(type: "text", nullable: false),
                    id_roles = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles_usuario", x => new { x.id_usuario, x.id_roles });
                    table.ForeignKey(
                        name: "FK_roles_usuario_roles_id_roles",
                        column: x => x.id_roles,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_roles_usuario_usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuario_cliente",
                columns: table => new
                {
                    cliente_id = table.Column<long>(type: "bigint", nullable: false),
                    id_usuario = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("usuario_cliente_pk", x => new { x.cliente_id, x.id_usuario });
                    table.ForeignKey(
                        name: "cliente_fk",
                        column: x => x.cliente_id,
                        principalTable: "cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "usuario_fk",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "produto_preco",
                columns: table => new
                {
                    id_produto_preco = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    preco = table.Column<List<decimal>>(type: "numeric(10,4)[]", nullable: false),
                    data_inicio = table.Column<DateTime>(type: "timestamp", nullable: false),
                    data_fim = table.Column<DateTime>(type: "timestamp", nullable: true),
                    id_fornecedor = table.Column<long>(type: "bigint", nullable: false),
                    id_produto_produto = table.Column<long>(type: "bigint", nullable: true),
                    client_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("produto_preco_pk", x => x.id_produto_preco);
                    table.ForeignKey(
                        name: "id_fornecedor_foreign_key",
                        column: x => x.id_fornecedor,
                        principalTable: "fornecedores",
                        principalColumn: "id_fornecedor");
                    table.ForeignKey(
                        name: "produto_fk",
                        column: x => x.id_produto_produto,
                        principalTable: "produto",
                        principalColumn: "id_produto",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "receita_versao_ingredientes",
                columns: table => new
                {
                    id_produto = table.Column<long>(type: "bigint", nullable: false),
                    id_receita_versao = table.Column<long>(type: "bigint", nullable: false),
                    quantidade = table.Column<decimal>(type: "numeric(10,4)", precision: 10, scale: 4, nullable: true),
                    unidade = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    client_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("receita_versao_ingredientes_pk", x => new { x.id_produto, x.id_receita_versao });
                    table.ForeignKey(
                        name: "id_produto_foreign_key",
                        column: x => x.id_produto,
                        principalTable: "produto",
                        principalColumn: "id_produto");
                    table.ForeignKey(
                        name: "id_receita_versao_foreign_key",
                        column: x => x.id_produto,
                        principalTable: "receitas_versao",
                        principalColumn: "id_receita_versao");
                });

            migrationBuilder.CreateIndex(
                name: "IX_produto_id_ingrediente",
                table: "produto",
                column: "id_ingrediente");

            migrationBuilder.CreateIndex(
                name: "IX_produto_id_marca",
                table: "produto",
                column: "id_marca");

            migrationBuilder.CreateIndex(
                name: "IX_produto_preco_id_fornecedor",
                table: "produto_preco",
                column: "id_fornecedor");

            migrationBuilder.CreateIndex(
                name: "IX_produto_preco_id_produto_produto",
                table: "produto_preco",
                column: "id_produto_produto");

            migrationBuilder.CreateIndex(
                name: "IX_receitas_versao_id_receitas",
                table: "receitas_versao",
                column: "id_receitas");

            migrationBuilder.CreateIndex(
                name: "IX_roles_usuario_id_roles",
                table: "roles_usuario",
                column: "id_roles");

            migrationBuilder.CreateIndex(
                name: "usuario_cliente_uq",
                table: "usuario_cliente",
                column: "id_usuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "funcionarios");

            migrationBuilder.DropTable(
                name: "produto_preco");

            migrationBuilder.DropTable(
                name: "receita_versao_ingredientes");

            migrationBuilder.DropTable(
                name: "roles_usuario");

            migrationBuilder.DropTable(
                name: "usuario_cliente");

            migrationBuilder.DropTable(
                name: "fornecedores");

            migrationBuilder.DropTable(
                name: "produto");

            migrationBuilder.DropTable(
                name: "receitas_versao");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "cliente");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "ingrediente");

            migrationBuilder.DropTable(
                name: "marca");

            migrationBuilder.DropTable(
                name: "receitas");
        }
    }
}
