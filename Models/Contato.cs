using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPessoas.Models
{
    public class Contato
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public string Endereco { get; set; }
    }
}
