using Microsoft.EntityFrameworkCore;
using PadocaGestor.Infrastructure.Models;

namespace PadocaGestor.Infrastructure.ConfigMaps
{
    internal class ProdutoPrecoMap : IEntityTypeConfiguration<ProdutoPreco>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProdutoPreco> builder)
        {
            builder.HasKey(e => e.IdProdutoPreco).HasName("produto_preco_pk");

            builder.ToTable("produto_preco");

            builder.Property(e => e.IdProdutoPreco)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_produto_preco");
            builder.Property(e => e.DataFim)
                .HasColumnType("timestamp")
                .HasColumnName("data_fim");
            builder.Property(e => e.DataInicio)
                .HasColumnType("timestamp")
                .HasColumnName("data_inicio");
            builder.Property(e => e.IdFornecedor).HasColumnName("id_fornecedor");
            builder.Property(e => e.IdProdutoProduto).HasColumnName("id_produto_produto");
            
            builder.Property(e=>e.Rendimento).HasColumnName("rendimento")
                .HasColumnType("numeric(10,4)")
                .HasColumnName("rendimento");
            
            builder.Property(e=>e.Preco).HasColumnName("preco")
                .HasColumnType("numeric(10,4)")
                .HasColumnName("preco");

            builder.HasOne(d => d.IdFornecedorNavigation).WithMany(p => p.ProdutoPrecos)
                .HasForeignKey(d => d.IdFornecedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_fornecedor_foreign_key");

            builder.HasOne(d => d.IdProdutoProdutoNavigation).WithMany(p => p.ProdutoPrecos)
                .HasForeignKey(d => d.IdProdutoProduto)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("produto_fk");
            
            builder.Property(e => e.IdCliente)
                .HasColumnName("client_id");
        }
    }
}
