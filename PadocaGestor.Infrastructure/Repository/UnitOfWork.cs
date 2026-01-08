using PadocaGestor.Infrastructure.Database;
using PadocaGestor.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PadocaGestor.Infrastructure.Repository
{
    public class UnitOfWork : IDisposable
    {

        public UnitOfWork(PadocaContext context)
        {
            Context = context;
        }

        public PadocaContext Context { get; }

        private Repository<Fornecedor> FornecedorRepository;
        private Repository<Funcionario> FuncionarioRepository;

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
