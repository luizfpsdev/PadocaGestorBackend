using Microsoft.EntityFrameworkCore;
using PadocaGestor.Infrastructure.Models;

namespace PadocaGestor.Infrastructure.ConfigMaps
{
    internal class ReceitaVersaoIngredienteMap : IEntityTypeConfiguration<ReceitaVersaoIngrediente>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ReceitaVersaoIngrediente> builder)
        {
            builder.HasKey(e => new { e.IdProduto, e.IdReceitaVersao }).HasName("receita_versao_ingredientes_pk");

            builder.ToTable("receita_versao_ingredientes");

            builder.Property(e => e.IdProduto).HasColumnName("id_produto");
            builder.Property(e => e.IdReceitaVersao).HasColumnName("id_receita_versao");
            builder.Property(e => e.Quantidade)
                .HasPrecision(10, 4)
                .HasColumnName("quantidade");
            builder.Property(e => e.Unidade)
                .HasMaxLength(50)
                .HasColumnName("unidade");

            builder.HasOne(d => d.IdProdutoNavigation).WithMany(p => p.ReceitaVersaoIngredientes)
                .HasForeignKey(d => d.IdProduto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_produto_foreign_key");

            builder.HasOne(d => d.ReceitaVersao).WithMany(p => p.ReceitaVersaoIngredientes)
                .HasForeignKey(d => d.IdProduto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_receita_versao_foreign_key");
        }
    }
}
