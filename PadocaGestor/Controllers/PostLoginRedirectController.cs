using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PadocaGestor.Application.Abstrations;

namespace PadocaGestor.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostLoginRedirectController : ControllerBase
    {
        private readonly IUsuarioAtual _usuarioAtual;

        public PostLoginRedirectController(IUsuarioAtual usuarioAtual)
        {
            _usuarioAtual = usuarioAtual;
        }
        
        [HttpGet]
        public ActionResult<string> Get()
        {
            var usuario = _usuarioAtual.ObterUsuario();
            
            return Ok(usuario);
        }
    }
}
