using System;
using System.Collections.Generic;

namespace PadocaGestor.Infrastructure.Models;

public partial class ProdutoPreco
{
    public long IdProdutoPreco { get; set; }

    public List<decimal> Preco { get; set; } = null!;

    public DateTime DataInicio { get; set; }

    public DateTime? DataFim { get; set; }

    public long IdFornecedor { get; set; }

    public long? IdProdutoProduto { get; set; }

    public virtual Fornecedor IdFornecedorNavigation { get; set; } = null!;

    public virtual Produto? IdProdutoProdutoNavigation { get; set; }
}
