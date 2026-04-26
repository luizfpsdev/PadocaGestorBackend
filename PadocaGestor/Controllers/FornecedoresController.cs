using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PadocaGestor.Application.Abstractions;
using PadocaGestor.Api.Contracts.Fornecedores;
using PadocaGestor.Api.Services.Fornecedores;

namespace PadocaGestor.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FornecedoresController : ControllerBase
    {
        private readonly IUsuarioAtual _usuarioAtual;
        private readonly IFornecedoresService _fornecedoresService;

        public FornecedoresController(
            IUsuarioAtual usuarioAtual,
            IFornecedoresService fornecedoresService)
        {
            _usuarioAtual = usuarioAtual;
            _fornecedoresService = fornecedoresService;
        }

        [HttpGet(Name = "Fornecedores")]
        public async Task<ActionResult<ListarFornecedoresResponse>> Get(
            [FromQuery] int pagina = 1,
            [FromQuery] int tamanhoPagina = 10,
            [FromQuery] string? nome = null,
            [FromQuery] string ordem = "asc")
        {
            var resultado = await _fornecedoresService.ListarAsync(
                new ListarFornecedoresRequest(pagina, tamanhoPagina, nome, ordem),
                _usuarioAtual.Id);

            return ToActionResult(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<FornecedorResponse>> Post([FromBody] CadastrarFornecedorRequest request)
        {
            var resultado = await _fornecedoresService.CadastrarAsync(request, _usuarioAtual.Id);
            return ToActionResult(resultado);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var resultado = await _fornecedoresService.ExcluirAsync(id, _usuarioAtual.Id);
            return ToActionResult(resultado);
        }

        private ActionResult<T> ToActionResult<T>(FornecedorOperacaoResultado<T> resultado)
        {
            return resultado.Status switch
            {
                FornecedorOperacaoStatus.Success => Ok(resultado.Dados),
                FornecedorOperacaoStatus.BadRequest => BadRequest(resultado.Mensagem),
                FornecedorOperacaoStatus.Unauthorized => Unauthorized(resultado.Mensagem),
                FornecedorOperacaoStatus.NotFound => NotFound(resultado.Mensagem),
                FornecedorOperacaoStatus.Conflict => Conflict(resultado.Mensagem),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };
        }

        private IActionResult ToActionResult(FornecedorOperacaoResultado resultado)
        {
            return resultado.Status switch
            {
                FornecedorOperacaoStatus.Success => NoContent(),
                FornecedorOperacaoStatus.BadRequest => BadRequest(resultado.Mensagem),
                FornecedorOperacaoStatus.Unauthorized => Unauthorized(resultado.Mensagem),
                FornecedorOperacaoStatus.NotFound => NotFound(resultado.Mensagem),
                FornecedorOperacaoStatus.Conflict => Conflict(resultado.Mensagem),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };
        }
    }
}
