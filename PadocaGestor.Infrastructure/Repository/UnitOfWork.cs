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

        private PadocaContext Context { get; }

        private Repository<Fornecedor>? _fornecedorRepository = null;
        private Repository<Funcionario>? _funcionarioRepository= null;
        private Repository<Ingrediente>? _ingredienteRepository= null;
        private Repository<Produto>? _produtoRepository= null;
        private Repository<ProdutoPreco>? _produtPrecoRepository= null;
        private Repository<Receita>? _receitaRepository= null;
        private Repository<ReceitasVersao>? _receitasVersaoRepository= null;
        private Repository<UsuarioCliente>? _usuarioClienteRepository= null;
        private Repository<Usuario>? _usuarioRepository= null;

        public Repository<Fornecedor> FornecedorRepository => _fornecedorRepository ?? new Repository<Fornecedor>(Context);

        public Repository<Funcionario> FuncionarioRepository => _funcionarioRepository ?? new Repository<Funcionario>(Context);

        public Repository<Ingrediente> IngredienteRepository => _ingredienteRepository ?? new Repository<Ingrediente>(Context);

        public Repository<Produto> ProdutoRepository => _produtoRepository ?? new Repository<Produto>(Context);

        public Repository<ProdutoPreco> ProdutoPrecoRepository => _produtPrecoRepository ?? new Repository<ProdutoPreco>(Context);

        public Repository<Receita> ReceitaRepository => _receitaRepository ?? new Repository<Receita>(Context);

        public Repository<ReceitasVersao> ReceitasVersaoRepository => _receitasVersaoRepository ?? new Repository<ReceitasVersao>(Context);
        public Repository<UsuarioCliente> UsuarioClienteRepository => _usuarioClienteRepository ?? new Repository<UsuarioCliente>(Context);
        public Repository<Usuario> UsuarioRepository => _usuarioRepository ?? new Repository<Usuario>(Context);

        public async Task CommitAsync() => await Context.SaveChangesAsync();

        public void Dispose()
        {
            
        }
    }
}
