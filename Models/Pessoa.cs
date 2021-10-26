using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPessoas.Models
{
    public class Pessoa
    {
        public IEnumerable<PessoaFisica> ListaPessoaFisica { get; set; }
        public IEnumerable<PessoaJuridica> ListaPessoaJuridica { get; set; }
        public IEnumerable<Sexo> ListaSexo { get; set; }
    }
}
