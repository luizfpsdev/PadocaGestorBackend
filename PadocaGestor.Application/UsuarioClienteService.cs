using PadocaGestor.Application.Abstractions;
using PadocaGestor.Infrastructure.Database;
using PadocaGestor.Infrastructure.Models;
using PadocaGestor.Infrastructure.Repository;

namespace PadocaGestor.Application;

public class UsuarioClienteService : IUsuarioClienteService
{
    private readonly UnitOfWork _unitOfWork;

    public UsuarioClienteService(PadocaContext context )
    {
        _unitOfWork = new UnitOfWork(context);
    }
    public async Task<object> ObterUsuarioClienteByUsuarioAsync(Guid? usuarioId)
    {
        var usuarioCliente = (await _unitOfWork.UsuarioClienteRepository.Get(x => x.IdUsuario == usuarioId.ToString())).SingleOrDefault();
        
        return usuarioCliente;
    }

    public async Task CriarUsuarioClienteAsync(Guid? usuarioId,string email)
    {
        var usuarioCliente = await ObterUsuarioClienteByUsuarioAsync(usuarioId);

        //caso usuarioCLiente for null então é o primeiro registro e habilitação do periodo trial
        if (usuarioCliente == null)
        {
            usuarioCliente = new UsuarioCliente
            {
                Ativo = true,
                CriadoEm = DateTime.UtcNow,
                IdUsuario = usuarioId.ToString(),
                Usuario = new Usuario
                {
                    Ativo = true,
                    CriadoEm = DateTime.UtcNow,
                    Email = email,
                   
                }
            };
        }
    }
}