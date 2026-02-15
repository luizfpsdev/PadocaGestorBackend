namespace PadocaGestor.Infrastructure.Models;

public partial class Usuario
{
    public string Id { get; set; } = null!;

    public bool Ativo { get; set; }

    public DateTime CriadoEm { get; set; }

    public virtual UsuarioCliente? UsuarioCliente { get; set; }
}
