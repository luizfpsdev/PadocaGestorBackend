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



    public virtual DbSet<UsuarioCliente> usuario_clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
   

      
        modelBuilder.Entity<UsuarioCliente>(entity =>
        {
           
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
