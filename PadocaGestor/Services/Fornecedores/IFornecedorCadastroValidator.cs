using PadocaGestor.Api.Contracts.Fornecedores;

namespace PadocaGestor.Api.Services.Fornecedores;

public interface IFornecedorCadastroValidator
{
    Task<FornecedorCadastroValidacaoResultado> ValidateAsync(
        CadastrarFornecedorRequest request,
        long idCliente);
}
