namespace PadocaGestor.Api.Services.Fornecedores;

public sealed class FornecedorCadastroValidacaoResultado
{
    private FornecedorCadastroValidacaoResultado(
        bool isValid,
        FornecedorCadastroDadosValidados? dados,
        FornecedorCadastroErroTipo? tipoErro,
        string? mensagemErro)
    {
        IsValid = isValid;
        Dados = dados;
        TipoErro = tipoErro;
        MensagemErro = mensagemErro;
    }

    public bool IsValid { get; }
    public FornecedorCadastroDadosValidados? Dados { get; }
    public FornecedorCadastroErroTipo? TipoErro { get; }
    public string? MensagemErro { get; }

    public static FornecedorCadastroValidacaoResultado Sucesso(FornecedorCadastroDadosValidados dados) =>
        new(true, dados, null, null);

    public static FornecedorCadastroValidacaoResultado Falha(
        FornecedorCadastroErroTipo tipoErro,
        string mensagemErro) =>
        new(false, null, tipoErro, mensagemErro);
}
