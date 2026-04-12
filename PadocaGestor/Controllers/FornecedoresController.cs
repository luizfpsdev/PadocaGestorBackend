using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PadocaGestor.Application.Abstractions;
using PadocaGestor.Infrastructure.Database;
using PadocaGestor.Infrastructure.Repository;

namespace PadocaGestor.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FornecedoresController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IUsuarioAtual _usuarioAtual;
        private readonly IUsuarioClienteService _usuarioClienteService;

        public FornecedoresController(
            PadocaContext context,
            IUsuarioAtual usuarioAtual,
            IUsuarioClienteService usuarioClienteService)
        {
            _unitOfWork = new UnitOfWork(context);
            _usuarioAtual = usuarioAtual;
            _usuarioClienteService = usuarioClienteService;
        }

        [HttpGet(Name = "Fornecedores")]
        public async Task<ActionResult<object>> Get(
            [FromQuery] int pagina = 1,
            [FromQuery] int tamanhoPagina = 10,
            [FromQuery] string? nome = null,
            [FromQuery] string ordem = "asc")
        {
            if (pagina < 1)
            {
                return BadRequest("O parametro pagina deve ser maior que zero.");
            }

            if (tamanhoPagina < 1)
            {
                return BadRequest("O parametro tamanhoPagina deve ser maior que zero.");
            }

            if (!string.Equals(ordem, "asc", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(ordem, "desc", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("O parametro ordem deve ser 'asc' ou 'desc'.");
            }

            var usuarioCliente = await _usuarioClienteService.ObterUsuarioClienteByUsuarioAsync(_usuarioAtual.Id);
            if (usuarioCliente is null)
            {
                return Unauthorized("Usuario sem cliente vinculado.");
            }

            var fornecedoresQuery = _unitOfWork.FornecedorRepository.Query(x =>
                x.IdCliente == usuarioCliente.IdCliente &&
                (string.IsNullOrWhiteSpace(nome) || (x.Nome != null && EF.Functions.ILike(x.Nome, $"%{nome.Trim()}%"))));

            fornecedoresQuery = string.Equals(ordem, "desc", StringComparison.OrdinalIgnoreCase)
                ? fornecedoresQuery.OrderByDescending(x => x.Nome ?? string.Empty)
                : fornecedoresQuery.OrderBy(x => x.Nome ?? string.Empty);

            var totalItens = await fornecedoresQuery.CountAsync();
            var itens = await fornecedoresQuery
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .Select(x => new
                {
                    x.IdFornecedor,
                    x.Nome,
                    x.Cnpj,
                    x.Ativo,
                    x.Observacao,
                    x.Endereco,
                    x.Cidade,
                    x.Uf,
                    x.Email,
                    x.Telefone
                })
                .ToListAsync();

            return Ok(new
            {
                pagina,
                tamanhoPagina,
                totalItens,
                totalPaginas = (int)Math.Ceiling(totalItens / (double)tamanhoPagina),
                ordem = ordem.ToLowerInvariant(),
                filtroNome = nome,
                itens
            });
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var usuarioCliente = await _usuarioClienteService.ObterUsuarioClienteByUsuarioAsync(_usuarioAtual.Id);
            if (usuarioCliente is null)
            {
                return Unauthorized("Usuario sem cliente vinculado.");
            }

            var fornecedor = (await _unitOfWork.FornecedorRepository
                    .Get(x => x.IdFornecedor == id && x.IdCliente == usuarioCliente.IdCliente))
                .SingleOrDefault();

            if (fornecedor is null)
            {
                return NotFound("Fornecedor nao encontrado.");
            }

            var fornecedorEmUso = (await _unitOfWork.ProdutoPrecoRepository
                .Get(x => x.IdFornecedor == id && x.IdCliente == usuarioCliente.IdCliente)).Any();

            if (fornecedorEmUso)
            {
                return Conflict("Fornecedor vinculado a precos de produtos e nao pode ser excluido.");
            }

            _unitOfWork.FornecedorRepository.Delete(fornecedor);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }
    }
}
