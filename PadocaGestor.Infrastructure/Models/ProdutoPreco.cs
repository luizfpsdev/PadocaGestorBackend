namespace PadocaGestor.Infrastructure.Models;

public partial class ProdutoPreco
{
    public long IdProdutoPreco { get; set; }

    public DateTime DataInicio { get; set; }
    
    public decimal Preco { get; set; }

    public DateTime? DataFim { get; set; }

    public long IdFornecedor { get; set; }

    public long? IdProdutoProduto { get; set; }
    
    public long IdCliente { get; set; }
    
    public decimal Rendimento { get; set; }

    public virtual Fornecedor IdFornecedorNavigation { get; set; } = null!;

    public virtual Produto? IdProdutoProdutoNavigation { get; set; }
}
