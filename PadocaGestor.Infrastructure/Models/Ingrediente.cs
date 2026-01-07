namespace PadocaGestor.Infrastructure.Models;

public partial class Ingrediente
{
    public long IdIngrediente { get; set; }

    public string? Nome { get; set; }

    public bool Ativo { get; set; }

    public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}
