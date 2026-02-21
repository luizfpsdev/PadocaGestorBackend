using Microsoft.EntityFrameworkCore;
using PadocaGestor.Infrastructure.Models;

namespace PadocaGestor.Infrastructure.ConfigMaps
{
    internal class RolesUsuarioMap : IEntityTypeConfiguration<RolesUsuario>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<RolesUsuario> builder)
        {
            builder.HasKey(e => new { e.UsuarioId, e.RolesId });

            builder.ToTable("roles_usuario");

            builder.Property(e => e.UsuarioId)
        
                .HasColumnName("id_usuario");

            builder.Property(e => e.RolesId)
                .HasColumnName("id_roles");
            
        }
    }
}
