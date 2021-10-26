using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPessoas.Models
{
    public class PessoaJuridica
    {
        public int Id { get; set; }
        public int IdContato { get; set; }

        public string Cnpj { get; set; }

        public string RazaoSocial { get; set; }

        public string NomeFantasia { get; set; }

        public string Endereco { get; set; }

        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}
