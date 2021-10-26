using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPessoas.Models
{
    public class PessoaFisica
    {
        public int Id { get; set; }
        public int IdSexo { get; set; }
        public int IdContato { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Rg { get; set; }

        public string Sexo { get; set; }
        public string Endereco { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }
    }
}
