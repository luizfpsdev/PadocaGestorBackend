using System.Security.Claims;
using PadocaGestor.Application.Abstractions;

namespace PadocaGestor.Api;

public class UsuarioAtual : IUsuarioAtual
{
    private static IHttpContextAccessor? _httpContextAccessor;

    public UsuarioAtual(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    public override string ToString()
    {
        return $"id:{Id}, email:{Email}, nome:{Nome}";
    }

    public Guid? Id =>
        Guid.Parse(_httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

    public string Email => _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value!;

    public string Nome => _httpContextAccessor?.HttpContext?.User?.FindFirst("name")?.Value!;
}