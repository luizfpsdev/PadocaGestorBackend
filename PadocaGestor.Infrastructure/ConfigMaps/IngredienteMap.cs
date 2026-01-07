using Microsoft.EntityFrameworkCore;
using PadocaGestor.Infrastructure.Models;

namespace PadocaGestor.Infrastructure.ConfigMaps
{
    internal class IngredienteMap : IEntityTypeConfiguration<Ingrediente>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Ingrediente> builder)
        {
            builder.HasKey(e => e.IdIngrediente).HasName("ingrediente_pk");

            builder.ToTable("ingrediente");

            builder.Property(e => e.IdIngrediente)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_ingrediente");
            builder.Property(e => e.Ativo).HasColumnName("ativo");
            builder.Property(e => e.Nome)
                .HasMaxLength(150)
                .HasColumnName("nome");
        }
    }
}
