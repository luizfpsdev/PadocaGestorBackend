using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PadocaGestor.Application.Abstractions;
using PadocaGestor.Infrastructure.Database;
using PadocaGestor.Infrastructure.Models;
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
                    x.Contato,
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

        [HttpPost]
        public async Task<ActionResult<object>> Post([FromBody] CadastrarFornecedorRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Nome))
            {
                return BadRequest("O campo nome e obrigatorio.");
            }

            if (string.IsNullOrWhiteSpace(request.Cidade))
            {
                return BadRequest("O campo cidade e obrigatorio.");
            }

            if (string.IsNullOrWhiteSpace(request.Uf))
            {
                return BadRequest("O campo uf e obrigatorio.");
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest("O campo email e obrigatorio.");
            }

            if (string.IsNullOrWhiteSpace(request.Contato))
            {
                return BadRequest("O campo contato e obrigatorio.");
            }

            var usuarioCliente = await _usuarioClienteService.ObterUsuarioClienteByUsuarioAsync(_usuarioAtual.Id);
            if (usuarioCliente is null)
            {
                return Unauthorized("Usuario sem cliente vinculado.");
            }

            var nome = request.Nome.Trim();
            var cidade = request.Cidade.Trim();
            var uf = request.Uf.Trim().ToUpperInvariant();
            var email = request.Email.Trim();
            var contato = request.Contato.Trim();
            var cnpj = string.IsNullOrWhiteSpace(request.Cnpj) ? null : request.Cnpj.Trim();
            var observacao = string.IsNullOrWhiteSpace(request.Observacao) ? null : request.Observacao.Trim();
            var endereco = string.IsNullOrWhiteSpace(request.Endereco) ? null : request.Endereco.Trim();
            var telefone = string.IsNullOrWhiteSpace(request.Telefone) ? null : request.Telefone.Trim();

            if (nome.Length > 255)
            {
                return BadRequest("O campo nome deve ter no maximo 255 caracteres.");
            }

            if (cnpj is not null && cnpj.Length > 15)
            {
                return BadRequest("O campo cnpj deve ter no maximo 15 caracteres.");
            }

            if (observacao is not null && observacao.Length > 255)
            {
                return BadRequest("O campo observacao deve ter no maximo 255 caracteres.");
            }

            if (telefone is not null && telefone.Length > 20)
            {
                return BadRequest("O campo telefone deve ter no maximo 20 caracteres.");
            }

            if (uf.Length != 2)
            {
                return BadRequest("O campo uf deve ter exatamente 2 caracteres.");
            }

            if (cnpj is not null)
            {
                var fornecedorComMesmoCnpj = await _unitOfWork.FornecedorRepository.Query(x =>
                        x.IdCliente == usuarioCliente.IdCliente &&
                        x.Cnpj != null &&
                        x.Cnpj == cnpj)
                    .AnyAsync();

                if (fornecedorComMesmoCnpj)
                {
                    return Conflict("Ja existe um fornecedor com este cnpj.");
                }
            }

            var fornecedor = new Fornecedor
            {
                Nome = nome,
                Cidade = cidade,
                Uf = uf,
                Email = email,
                Contato = contato,
                Cnpj = cnpj,
                Observacao = observacao,
                Endereco = endereco,
                Telefone = telefone,
                Ativo = request.Ativo ?? true,
                IdCliente = usuarioCliente.IdCliente
            };

            await _unitOfWork.FornecedorRepository.InsertAsync(fornecedor);
            await _unitOfWork.CommitAsync();

            return Ok(new
            {
                fornecedor.IdFornecedor,
                fornecedor.Contato,
                fornecedor.Nome,
                fornecedor.Cnpj,
                fornecedor.Ativo,
                fornecedor.Observacao,
                fornecedor.Endereco,
                fornecedor.Cidade,
                fornecedor.Uf,
                fornecedor.Email,
                fornecedor.Telefone
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

        public sealed record CadastrarFornecedorRequest(
            string? Nome,
            string? Cnpj,
            bool? Ativo,
            string? Observacao,
            string? Endereco,
            string? Cidade,
            string? Uf,
            string? Email,
            string? Telefone,
            string? Contato);
    }
}
