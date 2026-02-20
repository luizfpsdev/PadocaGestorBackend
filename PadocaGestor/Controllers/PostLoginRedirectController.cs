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
        public ActionResult<string> Get()
        {
            var usuario = _usuarioAtual.ObterUsuario();
            
            var usuarioJaExistente = await _usuarioClienteService.ObterUsuarioClienteByUsuarioAsync(usuario.Id);
            
            //TODO: verificar se existe dados na tabela usuario cliente com o id caso não exista quer dizer que é um usuário novo com role de admin e pelo menos trial
            
            return Ok(usuario);
        }
    }
}
