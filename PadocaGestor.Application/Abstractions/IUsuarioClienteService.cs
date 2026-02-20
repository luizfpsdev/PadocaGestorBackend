namespace PadocaGestor.Application.Abstractions;

public interface IUsuarioClienteService
{
    Task<object> ObterUsuarioClienteByUsuarioAsync(Guid? usuarioId);
}