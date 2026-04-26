using Microsoft.EntityFrameworkCore;
using PadocaGestor.Api.Contracts.Fornecedores;
using PadocaGestor.Infrastructure.Database;

namespace PadocaGestor.Api.Services.Fornecedores;

public sealed class FornecedorCadastroValidator : IFornecedorCadastroValidator
{
    private readonly PadocaContext _context;

    public FornecedorCadastroValidator(PadocaContext context)
    {
        _context = context;
    }

    public async Task<FornecedorCadastroValidacaoResultado> ValidateAsync(
        CadastrarFornecedorRequest request,
        long idCliente)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
        {
            return FornecedorCadastroValidacaoResultado.Falha(
                FornecedorCadastroErroTipo.BadRequest,
                "O campo nome e obrigatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.Cidade))
        {
            return FornecedorCadastroValidacaoResultado.Falha(
                FornecedorCadastroErroTipo.BadRequest,
                "O campo cidade e obrigatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.Uf))
        {
            return FornecedorCadastroValidacaoResultado.Falha(
                FornecedorCadastroErroTipo.BadRequest,
                "O campo uf e obrigatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            return FornecedorCadastroValidacaoResultado.Falha(
                FornecedorCadastroErroTipo.BadRequest,
                "O campo email e obrigatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.Contato))
        {
            return FornecedorCadastroValidacaoResultado.Falha(
                FornecedorCadastroErroTipo.BadRequest,
                "O campo contato e obrigatorio.");
        }

        var nome = request.Nome.Trim();
        var cidade = request.Cidade.Trim();
        var uf = request.Uf.Trim().ToUpperInvariant();
        var email = request.Email.Trim();
        var contato = request.Contato.Trim();
        var cnpj = NormalizarOpcional(request.Cnpj);
        var observacao = NormalizarOpcional(request.Observacao);
        var endereco = NormalizarOpcional(request.Endereco);
        var telefone = NormalizarOpcional(request.Telefone);

        if (nome.Length > 255)
        {
            return FornecedorCadastroValidacaoResultado.Falha(
                FornecedorCadastroErroTipo.BadRequest,
                "O campo nome deve ter no maximo 255 caracteres.");
        }

        if (cnpj is not null && cnpj.Length > 15)
        {
            return FornecedorCadastroValidacaoResultado.Falha(
                FornecedorCadastroErroTipo.BadRequest,
                "O campo cnpj deve ter no maximo 15 caracteres.");
        }

        if (observacao is not null && observacao.Length > 255)
        {
            return FornecedorCadastroValidacaoResultado.Falha(
                FornecedorCadastroErroTipo.BadRequest,
                "O campo observacao deve ter no maximo 255 caracteres.");
        }

        if (telefone is not null && telefone.Length > 20)
        {
            return FornecedorCadastroValidacaoResultado.Falha(
                FornecedorCadastroErroTipo.BadRequest,
                "O campo telefone deve ter no maximo 20 caracteres.");
        }

        if (uf.Length != 2)
        {
            return FornecedorCadastroValidacaoResultado.Falha(
                FornecedorCadastroErroTipo.BadRequest,
                "O campo uf deve ter exatamente 2 caracteres.");
        }

        if (cnpj is not null)
        {
            var fornecedorComMesmoCnpj = await _context.Fornecedores.AnyAsync(x =>
                x.IdCliente == idCliente &&
                x.Cnpj != null &&
                x.Cnpj == cnpj);

            if (fornecedorComMesmoCnpj)
            {
                return FornecedorCadastroValidacaoResultado.Falha(
                    FornecedorCadastroErroTipo.Conflict,
                    "Ja existe um fornecedor com este cnpj.");
            }
        }

        return FornecedorCadastroValidacaoResultado.Sucesso(new FornecedorCadastroDadosValidados(
            nome,
            cnpj,
            request.Ativo ?? true,
            observacao,
            endereco,
            cidade,
            uf,
            email,
            telefone,
            contato));
    }

    private static string? NormalizarOpcional(string? valor) =>
        string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();
}
