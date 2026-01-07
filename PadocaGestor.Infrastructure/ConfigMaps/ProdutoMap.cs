using Microsoft.EntityFrameworkCore;
using PadocaGestor.Infrastructure.Models;

namespace PadocaGestor.Infrastructure.ConfigMaps
{
    internal class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(e => e.IdProduto).HasName("produto_pk");

            builder.ToTable("produto");

            builder.Property(e => e.IdProduto)
            .UseIdentityAlwaysColumn()
            .HasColumnName("id_produto");
            builder.Property(e => e.Descricao)
                .HasMaxLength(200)
                .HasColumnName("descricao");
            builder.Property(e => e.IdIngrediente).HasColumnName("id_ingrediente");
            builder.Property(e => e.IdMarca).HasColumnName("id_marca");

            builder.HasOne(d => d.IdIngredienteNavigation).WithMany(p => p.Produtos)
            .HasForeignKey(d => d.IdIngrediente)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("id_ingrediente_foreign_key");

            builder.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Produtos)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("id_marca_foreign_key");
        }
    }
}
