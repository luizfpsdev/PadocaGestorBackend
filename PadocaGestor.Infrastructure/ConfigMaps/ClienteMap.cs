using Microsoft.EntityFrameworkCore;
using PadocaGestor.Infrastructure.Models;

namespace PadocaGestor.Infrastructure.ConfigMaps
{
    internal class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(e => e.Id).HasName("cliente_pk");

            builder.ToTable("cliente");

            builder.Property(e => e.Id).UseIdentityAlwaysColumn()
                .HasColumnName("id_cliente");
            builder.Property(e => e.CriadoEm).HasColumnName("criado_em")
                .HasColumnType("timestamp with time zone");
            builder.Property(e => e.Nome).HasMaxLength(200)
                .HasColumnName("nome");
            builder.Property(e => e.Status).HasColumnName("status");
        }
    }
}
