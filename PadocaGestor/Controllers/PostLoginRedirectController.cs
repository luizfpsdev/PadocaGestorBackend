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
            
            //TODO: verificar se existe dados na tabela usuario cliente com o id caso não exista quer dizer que é um usuário novo com role de admin e pelo menos trial
            
            return Ok(usuario);
        }
    }
}
