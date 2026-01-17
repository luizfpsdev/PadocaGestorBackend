using Microsoft.EntityFrameworkCore;
using PadocaGestor.Infrastructure.Models;

namespace PadocaGestor.Infrastructure.ConfigMaps
{
    internal class UsuarioClienteMap : IEntityTypeConfiguration<UsuarioCliente>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UsuarioCliente> builder)
        {
            builder.HasKey(e => new { e.IdCliente, e.IdUsuario }).HasName("usuario_cliente_pk");

            builder.ToTable("usuario_cliente");

            builder.HasIndex(e => e.IdUsuario, "usuario_cliente_uq").IsUnique();

            builder.Property(e => e.IdUsuario).HasMaxLength(50);
            builder.Property(e => e.CriadoEm).HasColumnType("timestamp without time zone");

            builder.HasOne(d => d.Cliente).WithMany(p => p.UsuarioClientes)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("cliente_fk");

            builder.HasOne(d => d.Usuario).WithOne(p => p.UsuarioCliente)
                .HasForeignKey<UsuarioCliente>(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("usuario_fk");
        }
    }
}
