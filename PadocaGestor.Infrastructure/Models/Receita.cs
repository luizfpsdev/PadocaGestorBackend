namespace PadocaGestor.Infrastructure.Models;

public partial class Receita
{
    public string Nome { get; set; } = null!;

    public long Id { get; set; }

    public string? Preparo { get; set; }
    
    public string? Descricao { get; set; }

    public DateTime? DataCriacao { get; set; }

    public long IdEmpresa { get; set; }

    public decimal? Rendimento { get; set; }
    
    public long IdCliente { get; set; }
    
    public int TipoPrecificacao { get; set; }
    
    public decimal PrecoVenda { get; set; }
    
    public decimal Markup { get; set; }

    public virtual ICollection<ReceitasVersao> ReceitasVersaos { get; set; } = new List<ReceitasVersao>();
}
