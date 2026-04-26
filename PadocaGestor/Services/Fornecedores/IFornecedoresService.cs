using PadocaGestor.Api.Contracts.Fornecedores;

namespace PadocaGestor.Api.Services.Fornecedores;

public interface IFornecedoresService
{
    Task<FornecedorOperacaoResultado<ListarFornecedoresResponse>> ListarAsync(
        ListarFornecedoresRequest request,
        Guid? usuarioId);

    Task<FornecedorOperacaoResultado<FornecedorResponse>> CadastrarAsync(
        CadastrarFornecedorRequest request,
        Guid? usuarioId);

    Task<FornecedorOperacaoResultado> ExcluirAsync(
        long fornecedorId,
        Guid? usuarioId);
}
