using PadocaGestor.Infrastructure.Models;

namespace PadocaGestor.Application.Abstractions;

public interface IUsuarioClienteService
{
    Task<UsuarioCliente?> ObterUsuarioClienteByUsuarioAsync(Guid? usuarioId);
    Task CriarUsuarioClienteAsync(Guid? usuarioId, string email, string nome);
}