using System;
using System.Collections.Generic;

namespace Business.Models
{
    public class Fornecedor : Entity
    {

        public string Nome { get; set; }

        public string Documento { get; set; }

        public TipoFornecedor TipoFornecedor { get; set; }

        public Endereco Endereco { get; set; }

        public bool Ativo { get; set; }

        /* EF Relations */

        /* 1 fornecedor tem varios produtos - 1 pra muitos */
        public IEnumerable<Produto> Produtos { get; set; }
    }
}
