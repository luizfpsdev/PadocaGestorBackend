using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PadocaGestor.Application.Abstractions;

namespace PadocaGestor.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostLoginRedirectController : ControllerBase
    {
        private readonly IUsuarioAtual _usuarioAtual;
        private readonly IUsuarioClienteService _usuarioClienteService;
        private readonly ILogger<PostLoginRedirectController> _logger;

        public PostLoginRedirectController(IUsuarioAtual usuarioAtual, IUsuarioClienteService usuarioClienteService,
            ILogger<PostLoginRedirectController> logger)
        {
            _usuarioAtual = usuarioAtual;
            _usuarioClienteService = usuarioClienteService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            await _usuarioClienteService.CriarUsuarioClienteAsync(_usuarioAtual.Id, _usuarioAtual.Email!,
                _usuarioAtual.Nome!);

            _logger.LogDebug(_usuarioAtual.ToString());

            return Ok();
        }
    }
}