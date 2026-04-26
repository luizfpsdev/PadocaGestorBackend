namespace PadocaGestor.Api.Contracts.Fornecedores;

public sealed record ListarFornecedoresResponse(
    int Pagina,
    int TamanhoPagina,
    int TotalItens,
    int TotalPaginas,
    string Ordem,
    string? FiltroNome,
    IReadOnlyCollection<FornecedorResponse> Itens);
