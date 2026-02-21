using Microsoft.EntityFrameworkCore;
using PadocaGestor.Infrastructure.Models;

namespace PadocaGestor.Infrastructure.ConfigMaps
{
    internal class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(e => e.Id).HasName("usuario_pk");

            builder.ToTable("usuario");

            builder.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            
            builder.Property(e => e.Ativo).HasColumnName("ativo");
            
            builder.Property(e => e.CriadoEm)
                 .HasColumnType("timestamp without time zone")
                .HasColumnName("criado_em");

            builder.Property(e => e.Email)
                .HasColumnType("email");

            builder.HasMany<RolesUsuario>(e => e.RolesUsuario);
        }
    }
}
