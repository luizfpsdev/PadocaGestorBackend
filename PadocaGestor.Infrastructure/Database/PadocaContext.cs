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

    public virtual DbSet<Fornecedor> Fornecedores { get; set; }

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
    }
}
