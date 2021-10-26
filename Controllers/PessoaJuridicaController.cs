using CadastroPessoas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CadastroPessoas.Dao;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CadastroPessoas.Controllers
{
    public class PessoaJuridicaController : Controller
    {
        private readonly ILogger<PessoaJuridicaController> _logger;
        private ContatoDao daoContato;
        private PessoaJuridicaDao pessoaJuridicaDao;
        public PessoaJuridicaController(ILogger<PessoaJuridicaController> logger)
        {
            daoContato = new ContatoDao();
            pessoaJuridicaDao = new PessoaJuridicaDao();
            _logger = logger;
        }

        [HttpPost]
        public ActionResult CadastrarPessoaJuridica(PessoaJuridica pessoaJuridica)
        {
            IEnumerable<PessoaJuridica> Lista = pessoaJuridicaDao.BuscarPorCnpj(pessoaJuridica.Cnpj);

            if (Lista.Count() == 0)
            {
                if (pessoaJuridica.Telefone != null || pessoaJuridica.Endereco != null || pessoaJuridica.Email != null)
                {
                    Contato contato = new Contato();
                    contato.Telefone = pessoaJuridica.Telefone;
                    contato.Endereco = pessoaJuridica.Endereco;
                    contato.Email = pessoaJuridica.Email;

                    var IdContato = daoContato.Inserir(contato);

                    pessoaJuridica.IdContato = IdContato;
                }
                pessoaJuridicaDao.Cadastrar(pessoaJuridica);
            }
            else
            {
                TempData["warning"] = "Cnpj já cadastrado!";
            }

            return RedirectToAction("Index", "Pessoa");
        }
    }
}
