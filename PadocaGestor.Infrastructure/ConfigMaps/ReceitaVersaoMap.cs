using Microsoft.EntityFrameworkCore;
using PadocaGestor.Infrastructure.Models;

namespace PadocaGestor.Infrastructure.ConfigMaps
{
    internal class ReceitaVersaoMap : IEntityTypeConfiguration<ReceitasVersao>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ReceitasVersao> builder)
        {
            builder.HasKey(e => e.IdReceitaVersao).HasName("receitas_versao_pk");

            builder.ToTable("receitas_versao");

            builder.Property(e => e.IdReceitaVersao)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_receita_versao");
            builder.Property(e => e.AlteradoPor)
                .HasMaxLength(100)
                .HasColumnName("alterado_por");
            builder.Property(e => e.Ativo).HasColumnName("ativo");
            builder.Property(e => e.DataVersao)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_versao");
            builder.Property(e => e.IdReceitas).HasColumnName("id_receitas");

            builder.HasOne(d => d.IdReceitasNavigation).WithMany(p => p.ReceitasVersaos)
                .HasForeignKey(d => d.IdReceitas)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("receitas_fk");
        }
    }
}
