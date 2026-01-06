using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PadocaGestor.Infrastructure.Models;

namespace PadocaGestor.Infrastructure.Database;

public partial class PadocaContext : DbContext
{
    public PadocaContext()
    {
    }

    public PadocaContext(DbContextOptions<PadocaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Fornecedore> Fornecedores { get; set; }

    public virtual DbSet<Funcionario> Funcionarios { get; set; }

    public virtual DbSet<Ingrediente> Ingredientes { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Produto> Produtos { get; set; }

    public virtual DbSet<ProdutoPreco> ProdutoPrecos { get; set; }

    public virtual DbSet<Receita> Receitas { get; set; }

    public virtual DbSet<ReceitaVersaoIngrediente> ReceitaVersaoIngredientes { get; set; }

    public virtual DbSet<ReceitasVersao> ReceitasVersaos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PadocaContext).Assembly);
        modelBuilder.Entity<Fornecedore>(entity =>
        {
            entity.HasKey(e => e.IdFornecedor).HasName("fornecedores_pk");

            entity.ToTable("fornecedores");

            entity.Property(e => e.IdFornecedor)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_fornecedor");
            entity.Property(e => e.Ativo).HasColumnName("ativo");
            entity.Property(e => e.Cnpj)
                .HasMaxLength(15)
                .HasColumnName("cnpj");
            entity.Property(e => e.Endereco)
                .HasColumnType("character varying")
                .HasColumnName("endereco");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Observacao)
                .HasMaxLength(255)
                .HasColumnName("observacao");
            entity.Property(e => e.Telefone)
                .HasMaxLength(20)
                .HasColumnName("telefone");
        });

        modelBuilder.Entity<Funcionario>(entity =>
        {
            entity.HasKey(e => e.IdFuncionario).HasName("funcionarios_pk");

            entity.ToTable("funcionarios");

            entity.Property(e => e.IdFuncionario)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_funcionario");
            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .HasColumnName("nome");
            entity.Property(e => e.Telefone)
                .HasMaxLength(20)
                .HasColumnName("telefone");
        });

        modelBuilder.Entity<Ingrediente>(entity =>
        {
            entity.HasKey(e => e.IdIngrediente).HasName("ingrediente_pk");

            entity.ToTable("ingrediente");

            entity.Property(e => e.IdIngrediente)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_ingrediente");
            entity.Property(e => e.Ativo).HasColumnName("ativo");
            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("marca_pk");

            entity.ToTable("marca");

            entity.Property(e => e.IdMarca)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_marca");
            entity.Property(e => e.Ativo).HasColumnName("ativo");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.IdProduto).HasName("produto_pk");

            entity.ToTable("produto");

            entity.Property(e => e.IdProduto)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_produto");
            entity.Property(e => e.Descricao)
                .HasMaxLength(200)
                .HasColumnName("descricao");
            entity.Property(e => e.IdIngrediente).HasColumnName("id_ingrediente");
            entity.Property(e => e.IdMarca).HasColumnName("id_marca");

            entity.HasOne(d => d.IdIngredienteNavigation).WithMany(p => p.Produtos)
                .HasForeignKey(d => d.IdIngrediente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_ingrediente_foreign_key");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Produtos)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("id_marca_foreign_key");
        });

        modelBuilder.Entity<ProdutoPreco>(entity =>
        {
            entity.HasKey(e => e.IdProdutoPreco).HasName("produto_preco_pk");

            entity.ToTable("produto_preco");

            entity.Property(e => e.IdProdutoPreco)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_produto_preco");
            entity.Property(e => e.DataFim)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_fim");
            entity.Property(e => e.DataInicio)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_inicio");
            entity.Property(e => e.IdFornecedor).HasColumnName("id_fornecedor");
            entity.Property(e => e.IdProdutoProduto).HasColumnName("id_produto_produto");
            entity.Property(e => e.Preco)
                .HasColumnType("numeric(10,4)[]")
                .HasColumnName("preco");

            entity.HasOne(d => d.IdFornecedorNavigation).WithMany(p => p.ProdutoPrecos)
                .HasForeignKey(d => d.IdFornecedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_fornecedor_foreign_key");

            entity.HasOne(d => d.IdProdutoProdutoNavigation).WithMany(p => p.ProdutoPrecos)
                .HasForeignKey(d => d.IdProdutoProduto)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("produto_fk");
        });

        modelBuilder.Entity<Receita>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("receitas_pk");

            entity.ToTable("receitas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.DataCriacao)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_criacao");
            entity.Property(e => e.IdEmpresa)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_empresa");
            entity.Property(e => e.Nome)
                .HasColumnType("character varying")
                .HasColumnName("nome");
            entity.Property(e => e.Preparo)
                .HasColumnType("character varying")
                .HasColumnName("preparo");
            entity.Property(e => e.Rendimento)
                .HasPrecision(10, 4)
                .HasColumnName("rendimento");
        });

        modelBuilder.Entity<ReceitaVersaoIngrediente>(entity =>
        {
            entity.HasKey(e => new { e.IdProduto, e.IdReceitaVersao }).HasName("receita_versao_ingredientes_pk");

            entity.ToTable("receita_versao_ingredientes");

            entity.Property(e => e.IdProduto).HasColumnName("id_produto");
            entity.Property(e => e.IdReceitaVersao).HasColumnName("id_receita_versao");
            entity.Property(e => e.Quantidade)
                .HasPrecision(10, 4)
                .HasColumnName("quantidade");
            entity.Property(e => e.Unidade)
                .HasMaxLength(50)
                .HasColumnName("unidade");

            entity.HasOne(d => d.IdProdutoNavigation).WithMany(p => p.ReceitaVersaoIngredientes)
                .HasForeignKey(d => d.IdProduto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_produto_foreign_key");

            entity.HasOne(d => d.IdProduto1).WithMany(p => p.ReceitaVersaoIngredientes)
                .HasForeignKey(d => d.IdProduto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_receita_versao_foreign_key");
        });

        modelBuilder.Entity<ReceitasVersao>(entity =>
        {
            entity.HasKey(e => e.IdReceitaVersao).HasName("receitas_versao_pk");

            entity.ToTable("receitas_versao");

            entity.Property(e => e.IdReceitaVersao)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_receita_versao");
            entity.Property(e => e.AlteradoPor)
                .HasMaxLength(100)
                .HasColumnName("alterado_por");
            entity.Property(e => e.Ativo).HasColumnName("ativo");
            entity.Property(e => e.DataVersao)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_versao");
            entity.Property(e => e.IdReceitas).HasColumnName("id_receitas");

            entity.HasOne(d => d.IdReceitasNavigation).WithMany(p => p.ReceitasVersaos)
                .HasForeignKey(d => d.IdReceitas)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("receitas_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
