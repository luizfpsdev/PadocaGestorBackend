using Microsoft.EntityFrameworkCore;
using PadocaGestor.Infrastructure.Models;

namespace PadocaGestor.Infrastructure.ConfigMaps
{
    internal class MarcaMap : IEntityTypeConfiguration<Marca>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Marca> builder)
        {
            builder.HasKey(e => e.IdMarca).HasName("marca_pk");

            builder.ToTable("marca");

            builder.Property(e => e.IdMarca)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_marca");
            builder.Property(e => e.Ativo).HasColumnName("ativo");
            builder.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
        }
    }
}
