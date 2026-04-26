namespace PadocaGestor.Api.Services.Fornecedores;

public sealed record FornecedorCadastroDadosValidados(
    string Nome,
    string? Cnpj,
    bool Ativo,
    string? Observacao,
    string? Endereco,
    string Cidade,
    string Uf,
    string Email,
    string? Telefone,
    string Contato);
