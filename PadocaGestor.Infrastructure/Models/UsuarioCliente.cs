using System;
using System.Collections.Generic;

namespace PadocaGestor.Infrastructure.Models;

public partial class UsuarioCliente
{
    public DateTime CriadoEm { get; set; }

    public bool? Ativo { get; set; }

    public long IdCliente { get; set; }

    public string IdUsuario { get; set; } = null!;

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
