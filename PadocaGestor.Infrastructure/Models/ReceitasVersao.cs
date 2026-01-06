using System;
using System.Collections.Generic;

namespace PadocaGestor.Infrastructure.Models;

public partial class ReceitasVersao
{
    public long IdReceitaVersao { get; set; }

    public long? IdReceitas { get; set; }

    public DateTime? DataVersao { get; set; }

    public string? AlteradoPor { get; set; }

    public bool Ativo { get; set; }

    public virtual Receita? IdReceitasNavigation { get; set; }

    public virtual ICollection<ReceitaVersaoIngrediente> ReceitaVersaoIngredientes { get; set; } = new List<ReceitaVersaoIngrediente>();
}
