using PadocaGestor.Infrastructure.Database;
using PadocaGestor.Infrastructure.Models;

namespace PadocaGestor.Infrastructure.Repository
{
    public class UnitOfWork : IDisposable
    {

        public UnitOfWork(PadocaContext context)
        {
            Context = context;
        }

        public PadocaContext Context { get; }

        private Repository<Fornecedor> fornecedorRepository;
        private Repository<Funcionario> funcionarioRepository;
        private Repository<Ingrediente> ingredienteRepository;
        private Repository<Marca> marcaRepository;
        private Repository<Produto> produtoRepository;
        private Repository<ProdutoPreco> produtPrecoRepository;
        private Repository<Receita> receitaRepository;
        private Repository<ReceitasVersao> receitasVersaoRepository;

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
