namespace PadocaGestor.Infrastructure.Models;

public partial class ReceitaVersaoIngrediente
{
    public long IdProduto { get; set; }

    public long IdReceitaVersao { get; set; }

    public decimal? Quantidade { get; set; }

    public string? Unidade { get; set; }

    public virtual ReceitasVersao ReceitaVersao { get; set; } = null!;

    public virtual Produto IdProdutoNavigation { get; set; } = null!;
}
