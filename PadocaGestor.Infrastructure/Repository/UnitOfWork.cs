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

        public Repository<Fornecedor> FornecedorRepository
        {
            get
            {
                return fornecedorRepository == null? new Repository<Fornecedor>(Context): fornecedorRepository; 
            }
        }

        public Repository<Funcionario> FuncionarioRepository
        {
            get
            {
                return funcionarioRepository == null ? new Repository<Funcionario>(Context) : funcionarioRepository;
            }
        }

        public Repository<Ingrediente> IngredienteRepository
        {
            get
            {
                return ingredienteRepository == null ? new Repository<Ingrediente>(Context) : ingredienteRepository;
            }
        }

        public Repository<Marca> MarcaRepository
        {
            get
            {
                return marcaRepository == null ? new Repository<Marca>(Context) : marcaRepository;
            }
        }

        public Repository<Produto> ProdutoRepository
        {
            get
            {
                return produtoRepository == null ? new Repository<Produto>(Context) : produtoRepository;
            }
        }
        public Repository<ProdutoPreco> ProdutoPrecoRepository
        {
            get
            {
                return produtPrecoRepository == null ? new Repository<ProdutoPreco>(Context) : produtPrecoRepository;
            }
        }
        public Repository<Receita> ReceitaRepository
        {
            get
            {
                return receitaRepository == null ? new Repository<Receita>(Context) : receitaRepository;
            }
        }
        public Repository<ReceitasVersao> ReceitasVersaoRepository
        {
            get
            {
                return receitasVersaoRepository == null ? new Repository<ReceitasVersao>(Context) : receitasVersaoRepository;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
