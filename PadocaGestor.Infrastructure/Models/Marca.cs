using System;
using System.Collections.Generic;

namespace PadocaGestor.Infrastructure.Models;

public partial class Marca
{
    public long IdMarca { get; set; }

    public string Nome { get; set; } = null!;

    public bool Ativo { get; set; }

    public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}
