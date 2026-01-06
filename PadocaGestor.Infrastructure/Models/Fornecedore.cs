using System;
using System.Collections.Generic;

namespace PadocaGestor.Infrastructure.Models;

public partial class Fornecedore
{
    public long IdFornecedor { get; set; }

    public string? Nome { get; set; }

    public string? Cnpj { get; set; }

    public bool? Ativo { get; set; }

    public string? Observacao { get; set; }

    public string? Endereco { get; set; }

    public string? Telefone { get; set; }

    public virtual ICollection<ProdutoPreco> ProdutoPrecos { get; set; } = new List<ProdutoPreco>();
}
