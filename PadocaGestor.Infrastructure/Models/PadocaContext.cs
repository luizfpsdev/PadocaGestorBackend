using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PadocaGestor.Infrastructure.Models;

public partial class PadocaContext : DbContext
{
    public PadocaContext(DbContextOptions<PadocaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<cliente> clientes { get; set; }

    public virtual DbSet<usuario> usuarios { get; set; }

    public virtual DbSet<UsuarioCliente> usuario_clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<cliente>(entity =>
        {
            entity.HasKey(e => e.id_cliente).HasName("cliente_pk");

            entity.ToTable("cliente");

            entity.Property(e => e.id_cliente).UseIdentityAlwaysColumn();
            entity.Property(e => e.criado_em).HasColumnType("timestamp without time zone");
            entity.Property(e => e.nome).HasMaxLength(200);
        });

        modelBuilder.Entity<usuario>(entity =>
        {
            entity.HasKey(e => e.id).HasName("usuario_pk");

            entity.ToTable("usuario");

            entity.Property(e => e.id).HasMaxLength(50);
            entity.Property(e => e.criado_em).HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<UsuarioCliente>(entity =>
        {
            entity.HasKey(e => new { e.cliente_id, e.id_usuario }).HasName("usuario_cliente_pk");

            entity.ToTable("usuario_cliente");

            entity.HasIndex(e => e.id_usuario, "usuario_cliente_uq").IsUnique();

            entity.Property(e => e.id_usuario).HasMaxLength(50);
            entity.Property(e => e.criado_em).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.cliente).WithMany(p => p.usuario_clientes)
                .HasForeignKey(d => d.cliente_id)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("cliente_fk");

            entity.HasOne(d => d.id_usuarioNavigation).WithOne(p => p.usuario_cliente)
                .HasForeignKey<UsuarioCliente>(d => d.id_usuario)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("usuario_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
