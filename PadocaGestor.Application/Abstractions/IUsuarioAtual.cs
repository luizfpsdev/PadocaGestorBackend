namespace PadocaGestor.Application.Abstractions;

public interface IUsuarioAtual
{
    string ToString();
    Guid? Id {get; }
    string Email {get; }
    string Nome {get; }
}