using Microsoft.EntityFrameworkCore;
using PadocaGestor.Application.Abstractions;
using PadocaGestor.Api.Contracts.Fornecedores;
using PadocaGestor.Infrastructure.Database;
using PadocaGestor.Infrastructure.Models;
using PadocaGestor.Infrastructure.Repository;

namespace PadocaGestor.Api.Services.Fornecedores;

public sealed class FornecedoresService : IFornecedoresService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IUsuarioClienteService _usuarioClienteService;
    private readonly IFornecedorCadastroValidator _fornecedorCadastroValidator;

    public FornecedoresService(
        PadocaContext context,
        IUsuarioClienteService usuarioClienteService,
        IFornecedorCadastroValidator fornecedorCadastroValidator)
    {
        _unitOfWork = new UnitOfWork(context);
        _usuarioClienteService = usuarioClienteService;
        _fornecedorCadastroValidator = fornecedorCadastroValidator;
    }

    public async Task<FornecedorOperacaoResultado<ListarFornecedoresResponse>> ListarAsync(
        ListarFornecedoresRequest request,
        Guid? usuarioId)
    {
        if (request.Pagina < 1)
        {
            return FornecedorOperacaoResultado<ListarFornecedoresResponse>.Falha(
                FornecedorOperacaoStatus.BadRequest,
                "O parametro pagina deve ser maior que zero.");
        }

        if (request.TamanhoPagina < 1)
        {
            return FornecedorOperacaoResultado<ListarFornecedoresResponse>.Falha(
                FornecedorOperacaoStatus.BadRequest,
                "O parametro tamanhoPagina deve ser maior que zero.");
        }

        if (!OrdemValida(request.Ordem))
        {
            return FornecedorOperacaoResultado<ListarFornecedoresResponse>.Falha(
                FornecedorOperacaoStatus.BadRequest,
                "O parametro ordem deve ser 'asc' ou 'desc'.");
        }

        var usuarioCliente = await ObterUsuarioClienteAsync(usuarioId);
        if (usuarioCliente is null)
        {
            return FornecedorOperacaoResultado<ListarFornecedoresResponse>.Falha(
                FornecedorOperacaoStatus.Unauthorized,
                "Usuario sem cliente vinculado.");
        }

        var filtroNome = request.Nome?.Trim();
        var fornecedoresQuery = _unitOfWork.FornecedorRepository.Query(x =>
            x.IdCliente == usuarioCliente.IdCliente &&
            (string.IsNullOrWhiteSpace(filtroNome) || (x.Nome != null && EF.Functions.ILike(x.Nome, $"%{filtroNome}%"))));

        fornecedoresQuery = string.Equals(request.Ordem, "desc", StringComparison.OrdinalIgnoreCase)
            ? fornecedoresQuery.OrderByDescending(x => x.Nome ?? string.Empty)
            : fornecedoresQuery.OrderBy(x => x.Nome ?? string.Empty);

        var totalItens = await fornecedoresQuery.CountAsync();
        var itens = await fornecedoresQuery
            .Skip((request.Pagina - 1) * request.TamanhoPagina)
            .Take(request.TamanhoPagina)
            .Select(x => MapearFornecedor(x))
            .ToListAsync();

        var response = new ListarFornecedoresResponse(
            request.Pagina,
            request.TamanhoPagina,
            totalItens,
            (int)Math.Ceiling(totalItens / (double)request.TamanhoPagina),
            request.Ordem.ToLowerInvariant(),
            filtroNome,
            itens);

        return FornecedorOperacaoResultado<ListarFornecedoresResponse>.Sucesso(response);
    }

    public async Task<FornecedorOperacaoResultado<FornecedorResponse>> CadastrarAsync(
        CadastrarFornecedorRequest request,
        Guid? usuarioId)
    {
        var usuarioCliente = await ObterUsuarioClienteAsync(usuarioId);
        if (usuarioCliente is null)
        {
            return FornecedorOperacaoResultado<FornecedorResponse>.Falha(
                FornecedorOperacaoStatus.Unauthorized,
                "Usuario sem cliente vinculado.");
        }

        var validacao = await _fornecedorCadastroValidator.ValidateAsync(request, usuarioCliente.IdCliente);
        if (!validacao.IsValid)
        {
            var status = validacao.TipoErro == FornecedorCadastroErroTipo.Conflict
                ? FornecedorOperacaoStatus.Conflict
                : FornecedorOperacaoStatus.BadRequest;

            return FornecedorOperacaoResultado<FornecedorResponse>.Falha(
                status,
                validacao.MensagemErro!);
        }

        var dados = validacao.Dados!;
        var fornecedor = new Fornecedor
        {
            Nome = dados.Nome,
            Cidade = dados.Cidade,
            Uf = dados.Uf,
            Email = dados.Email,
            Contato = dados.Contato,
            Cnpj = dados.Cnpj,
            Observacao = dados.Observacao,
            Endereco = dados.Endereco,
            Telefone = dados.Telefone,
            Ativo = dados.Ativo,
            IdCliente = usuarioCliente.IdCliente
        };

        await _unitOfWork.FornecedorRepository.InsertAsync(fornecedor);
        await _unitOfWork.CommitAsync();

        return FornecedorOperacaoResultado<FornecedorResponse>.Sucesso(MapearFornecedor(fornecedor));
    }

    public async Task<FornecedorOperacaoResultado> ExcluirAsync(long fornecedorId, Guid? usuarioId)
    {
        var usuarioCliente = await ObterUsuarioClienteAsync(usuarioId);
        if (usuarioCliente is null)
        {
            return FornecedorOperacaoResultado.Falha(
                FornecedorOperacaoStatus.Unauthorized,
                "Usuario sem cliente vinculado.");
        }

        var fornecedor = (await _unitOfWork.FornecedorRepository
                .Get(x => x.IdFornecedor == fornecedorId && x.IdCliente == usuarioCliente.IdCliente))
            .SingleOrDefault();

        if (fornecedor is null)
        {
            return FornecedorOperacaoResultado.Falha(
                FornecedorOperacaoStatus.NotFound,
                "Fornecedor nao encontrado.");
        }

        var fornecedorEmUso = (await _unitOfWork.ProdutoPrecoRepository
            .Get(x => x.IdFornecedor == fornecedorId && x.IdCliente == usuarioCliente.IdCliente)).Any();

        if (fornecedorEmUso)
        {
            return FornecedorOperacaoResultado.Falha(
                FornecedorOperacaoStatus.Conflict,
                "Fornecedor vinculado a precos de produtos e nao pode ser excluido.");
        }

        _unitOfWork.FornecedorRepository.Delete(fornecedor);
        await _unitOfWork.CommitAsync();

        return FornecedorOperacaoResultado.Sucesso();
    }

    private async Task<PadocaGestor.Infrastructure.Models.UsuarioCliente?> ObterUsuarioClienteAsync(Guid? usuarioId) =>
        await _usuarioClienteService.ObterUsuarioClienteByUsuarioAsync(usuarioId);

    private static bool OrdemValida(string ordem) =>
        string.Equals(ordem, "asc", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(ordem, "desc", StringComparison.OrdinalIgnoreCase);

    private static FornecedorResponse MapearFornecedor(Fornecedor fornecedor) =>
        new(
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
            fornecedor.Telefone);
}
