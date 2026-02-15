namespace PadocaGestor.Infrastructure.Models;

public partial class Cliente
{
    public long Id { get; set; }

    public string Nome { get; set; } = null!;

    public int Status { get; set; }

    public DateTime CriadoEm { get; set; }

    public virtual ICollection<UsuarioCliente> UsuarioClientes { get; set; } = new List<UsuarioCliente>();
}
