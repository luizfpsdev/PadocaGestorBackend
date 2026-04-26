namespace PadocaGestor.Api.Contracts.Fornecedores;

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
