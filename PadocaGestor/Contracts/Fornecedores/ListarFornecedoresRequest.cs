namespace PadocaGestor.Api.Contracts.Fornecedores;

public sealed record ListarFornecedoresRequest(
    int Pagina,
    int TamanhoPagina,
    string? Nome,
    string Ordem);
