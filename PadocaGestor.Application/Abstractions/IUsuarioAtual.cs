using PadocaGestor.Domain;

namespace PadocaGestor.Application.Abstrations;

public interface IUsuarioAtual
{
    public UsuarioLogado ObterUsuario();
}