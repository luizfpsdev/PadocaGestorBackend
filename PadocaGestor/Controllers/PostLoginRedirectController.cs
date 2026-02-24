using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PadocaGestor.Application.Abstractions;
using PadocaGestor.Application.Abstrations;

namespace PadocaGestor.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostLoginRedirectController : ControllerBase
    {
        private readonly IUsuarioAtual _usuarioAtual;
        private readonly IUsuarioClienteService _usuarioClienteService;

        public PostLoginRedirectController(IUsuarioAtual usuarioAtual,IUsuarioClienteService usuarioClienteService)
        {
            _usuarioAtual = usuarioAtual;
            _usuarioClienteService = usuarioClienteService;
        }
        
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            var usuario = _usuarioAtual.ObterUsuario();
            
            await _usuarioClienteService.CriarUsuarioClienteAsync(usuario.Id,usuario.Email!,usuario.Nome!);
            
            
            return Ok(usuario);
        }
    }
}
