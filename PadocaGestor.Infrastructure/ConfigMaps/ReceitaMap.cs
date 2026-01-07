using Microsoft.EntityFrameworkCore;
using PadocaGestor.Infrastructure.Models;

namespace PadocaGestor.Infrastructure.ConfigMaps
{
    internal class ReceitaMap : IEntityTypeConfiguration<Receita>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Receita> builder)
        {
            builder.HasKey(e => e.Id).HasName("receitas_pk");

            builder.ToTable("receitas");

            builder.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            builder.Property(e => e.DataCriacao)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_criacao");
            builder.Property(e => e.IdEmpresa)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_empresa");
            builder.Property(e => e.Nome)
                .HasColumnType("character varying")
                .HasColumnName("nome");
            builder.Property(e => e.Preparo)
                .HasColumnType("character varying")
                .HasColumnName("preparo");
            builder.Property(e => e.Rendimento)
                .HasPrecision(10, 4)
                .HasColumnName("rendimento");
        }
    }
}
