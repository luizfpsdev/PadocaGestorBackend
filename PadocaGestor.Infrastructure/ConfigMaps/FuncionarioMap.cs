using Microsoft.EntityFrameworkCore;
using PadocaGestor.Infrastructure.Models;

namespace PadocaGestor.Infrastructure.ConfigMaps
{
    internal class FuncionarioMap : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Funcionario> builder)
        {
            builder.HasKey(e => e.IdFuncionario).HasName("funcionarios_pk");

            builder.ToTable("funcionarios");

            builder.Property(e => e.IdFuncionario)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_funcionario");
            builder.Property(e => e.Nome)
                .HasMaxLength(150)
                .HasColumnName("nome");
            builder.Property(e => e.Telefone)
                .HasMaxLength(20)
                .HasColumnName("telefone");
        }
    }
}
