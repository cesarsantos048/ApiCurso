using Business.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Task<Fornecedor> ObterFornecedorEndereco(Guid id);
        Task<IEnumerable<Fornecedor>> ObterAtivos();
        Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id);
    }
}
