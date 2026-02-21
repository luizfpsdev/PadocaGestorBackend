using Microsoft.EntityFrameworkCore;
using PadocaGestor.Infrastructure.Models;

namespace PadocaGestor.Infrastructure.ConfigMaps
{
    internal class RoleMap : IEntityTypeConfiguration<Roles>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Roles> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("roles");

            builder.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");

            builder.Property(e => e.Role)
                .HasColumnType("role");
            
            builder.HasMany<RolesUsuario>(e => e.RolesUsuarios);
        }
    }
}
