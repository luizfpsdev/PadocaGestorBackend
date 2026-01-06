using System;
using System.Collections.Generic;

namespace PadocaGestor.Infrastructure.Models;

public partial class Receita
{
    public string Nome { get; set; } = null!;

    public long Id { get; set; }

    public string? Preparo { get; set; }

    public DateTime? DataCriacao { get; set; }

    public long IdEmpresa { get; set; }

    public decimal? Rendimento { get; set; }

    public virtual ICollection<ReceitasVersao> ReceitasVersaos { get; set; } = new List<ReceitasVersao>();
}
