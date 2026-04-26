namespace PadocaGestor.Api.Services.Fornecedores;

public class FornecedorOperacaoResultado
{
    protected FornecedorOperacaoResultado(FornecedorOperacaoStatus status, string? mensagem)
    {
        Status = status;
        Mensagem = mensagem;
    }

    public FornecedorOperacaoStatus Status { get; }
    public string? Mensagem { get; }

    public static FornecedorOperacaoResultado Sucesso() =>
        new(FornecedorOperacaoStatus.Success, null);

    public static FornecedorOperacaoResultado Falha(FornecedorOperacaoStatus status, string mensagem) =>
        new(status, mensagem);
}

public sealed class FornecedorOperacaoResultado<T> : FornecedorOperacaoResultado
{
    private FornecedorOperacaoResultado(FornecedorOperacaoStatus status, string? mensagem, T? dados)
        : base(status, mensagem)
    {
        Dados = dados;
    }

    public T? Dados { get; }

    public static FornecedorOperacaoResultado<T> Sucesso(T dados) =>
        new(FornecedorOperacaoStatus.Success, null, dados);

    public static new FornecedorOperacaoResultado<T> Falha(FornecedorOperacaoStatus status, string mensagem) =>
        new(status, mensagem, default);
}
