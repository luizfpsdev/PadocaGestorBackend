namespace PadocaGestor.Application.Abstractions;

public interface IUsuarioClienteService
{
    Task<object> ObterUsuarioClienteByUsuarioAsync(Guid? usuarioId);
    Task CriarUsuarioClienteAsync(Guid? usuarioId, string email);
}