using System;
using System.Collections.Generic;

namespace PadocaGestor.Infrastructure.Models;

public partial class Produto
{
    public long IdProduto { get; set; }

    public long IdIngrediente { get; set; }

    public string? Descricao { get; set; }

    public long? IdMarca { get; set; }

    public virtual Ingrediente IdIngredienteNavigation { get; set; } = null!;

    public virtual Marca? IdMarcaNavigation { get; set; }

    public virtual ICollection<ProdutoPreco> ProdutoPrecos { get; set; } = new List<ProdutoPreco>();

    public virtual ICollection<ReceitaVersaoIngrediente> ReceitaVersaoIngredientes { get; set; } = new List<ReceitaVersaoIngrediente>();
}
