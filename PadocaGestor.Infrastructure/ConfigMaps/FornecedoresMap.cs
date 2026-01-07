using Microsoft.EntityFrameworkCore;
using PadocaGestor.Infrastructure.Models;

namespace PadocaGestor.Infrastructure.ConfigMaps
{
    internal class FornecedoresMap : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(e => e.IdFornecedor).HasName("fornecedores_pk");

            builder.ToTable("fornecedores");

            builder.Property(e => e.IdFornecedor)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_fornecedor");
            builder.Property(e => e.Ativo).HasColumnName("ativo");
            builder.Property(e => e.Cnpj)
                .HasMaxLength(15)
                .HasColumnName("cnpj");
            builder.Property(e => e.Endereco)
                .HasColumnType("character varying")
                .HasColumnName("endereco");
            builder.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            builder.Property(e => e.Observacao)
                .HasMaxLength(255)
                .HasColumnName("observacao");
            builder.Property(e => e.Telefone)
                .HasMaxLength(20)
                .HasColumnName("telefone");
        }
    }
}
