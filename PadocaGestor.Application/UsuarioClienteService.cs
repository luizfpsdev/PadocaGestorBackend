using PadocaGestor.Application.Abstractions;
using PadocaGestor.Domain;
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
    public async Task<UsuarioCliente?> ObterUsuarioClienteByUsuarioAsync(Guid? usuarioId)
    {
        var usuarioCliente = (await _unitOfWork.UsuarioClienteRepository.Get(x => x.IdUsuario == usuarioId.ToString())).SingleOrDefault();
        
        return usuarioCliente;
    }

    public async Task CriarUsuarioClienteAsync(Guid? usuarioId,string email,string nome)
    {
        var usuarioCliente = await ObterUsuarioClienteByUsuarioAsync(usuarioId);

        //caso usuarioCLiente for null então é o primeiro registro e habilitação do periodo trial
        if (usuarioCliente == null)
        {
            
            usuarioCliente = new UsuarioCliente
            {
                Ativo = true,
                CriadoEm = DateTime.UtcNow,
                Cliente = new Cliente
                {
                    CriadoEm = DateTime.UtcNow,
                    Nome = nome,
                    Status = 1
                },
                IdUsuario = usuarioId.ToString()!,
                Usuario = new Usuario
                {
                    Id =  usuarioId.ToString()!,
                    
                    Ativo = true,
                    CriadoEm = DateTime.UtcNow,
                    Email = email,
                }
            };
            
            await _unitOfWork.UsuarioClienteRepository.InsertAsync(usuarioCliente);
            await _unitOfWork.CommitAsync();
        }
    }
}