using PadocaGestor.Application.Abstractions;
using PadocaGestor.Infrastructure.Database;
using PadocaGestor.Infrastructure.Repository;

namespace PadocaGestor.Application;

public class UsuarioClienteService : IUsuarioClienteService
{
    private readonly UnitOfWork _unitOfWork;

    public UsuarioClienteService(PadocaContext context )
    {
        _unitOfWork = new UnitOfWork(context);
    }
    public Task<object> ObterUsuarioClienteByUsuarioAsync(Guid? usuarioId)
    {
        return null;
    }
}