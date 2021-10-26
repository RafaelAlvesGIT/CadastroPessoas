using CadastroPessoas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using CadastroPessoas.Dao;
using System.Linq;

namespace CadastroPessoas.Controllers
{
    public class PessoaController : Controller
    {
        private readonly ILogger<PessoaController> _logger;
        private SexoController sexoController;
        private PessoaFisicaDao pessoaFisicaDao;
        private PessoaJuridicaDao pessoaJuridicaDao;
        public PessoaController(ILogger<PessoaController> logger)
        {
            sexoController = new SexoController();
            pessoaFisicaDao = new PessoaFisicaDao();
            pessoaJuridicaDao = new PessoaJuridicaDao();
            _logger = logger;
        }

        public IActionResult Index(Pessoa ListaPessoa)
        {
            ListaPessoa.ListaSexo = sexoController.BuscarSexo();

            if (ListaPessoa.ListaPessoaFisica == null)
            {
                ListaPessoa.ListaPessoaFisica = pessoaFisicaDao.Listar();
            }

            if(ListaPessoa.ListaPessoaJuridica == null)
            {
                ListaPessoa.ListaPessoaJuridica = pessoaJuridicaDao.Listar();
            }
            return View(ListaPessoa);
        }

        [HttpPost]
        public ActionResult Buscar([FromForm] string Busca)
        {
            PessoaFisica pessoaFisica = new PessoaFisica();
            pessoaFisica.Nome = Busca;
            pessoaFisica.Rg = Busca;
            pessoaFisica.Cpf = Busca;

            IEnumerable<PessoaFisica> ListaPessoaFisica = pessoaFisicaDao.BuscarDados(pessoaFisica);

            PessoaJuridica pessoaJuridica = new PessoaJuridica();
            pessoaJuridica.Cnpj = Busca;
            pessoaJuridica.NomeFantasia = Busca;

            IEnumerable<PessoaJuridica> ListaPessoaJuridica = pessoaJuridicaDao.BuscarDados(pessoaJuridica);

            Pessoa pessoa = new Pessoa();
            pessoa.ListaPessoaFisica = ListaPessoaFisica;
            pessoa.ListaPessoaJuridica = ListaPessoaJuridica;

            return View("Index", pessoa);
        }
    }
}
