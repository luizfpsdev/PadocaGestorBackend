using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PadocaGestor.Application.Abstrations;
using PadocaGestor.Domain;

namespace PadocaGestor.Infrastructure;

public class UsuarioAtual : IUsuarioAtual
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsuarioAtual(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public UsuarioLogado ObterUsuario()
    {
        return new UsuarioLogado
        {
            Id = Guid.Parse(_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value!),
            Email = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value!,
            Nome = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value
            
        };
    }
}