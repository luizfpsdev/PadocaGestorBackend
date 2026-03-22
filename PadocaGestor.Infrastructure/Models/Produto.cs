namespace PadocaGestor.Infrastructure.Models;

public partial class Produto
{
    public long IdProduto { get; set; }

    public long IdIngrediente { get; set; }

    public string? Descricao { get; set; }
    
    public string? Nome { get; set; }

    public long IdCliente { get; set; }

    public virtual Ingrediente IdIngredienteNavigation { get; set; } = null!;

    public virtual ICollection<ProdutoPreco> ProdutoPrecos { get; set; } = new List<ProdutoPreco>();

    public virtual ICollection<ReceitaVersaoIngrediente> ReceitaVersaoIngredientes { get; set; } = new List<ReceitaVersaoIngrediente>();
}
