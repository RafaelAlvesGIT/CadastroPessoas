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
    public class PessoaFisicaController : Controller
    {
        private readonly ILogger<PessoaFisicaController> _logger;
        private SexoDao daoSexo;
        private PessoaFisicaDao pessoaFisicaDao;
        private ContatoDao daoContato;
        public PessoaFisicaController(ILogger<PessoaFisicaController> logger)
        {
            pessoaFisicaDao = new PessoaFisicaDao();
            daoSexo = new SexoDao();
            daoContato = new ContatoDao();
            _logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<Sexo> ListaSexo = daoSexo.Listar();
            ViewBag.ListaSexo = ListaSexo;
           
            IEnumerable<PessoaFisica> ListaPessoaFisica = pessoaFisicaDao.Listar();

            ViewBag.ListaPessoaFisica = ListaPessoaFisica;
            
            return View();
        }

        [HttpPost]
        public RedirectToActionResult CadastrarPessoa(PessoaFisica pessoaFisica)
        {            
            IEnumerable<PessoaFisica> Lista = pessoaFisicaDao.BuscarPorCpf(pessoaFisica.Cpf);

            if (Lista.Count() == 0)
            {
                if (pessoaFisica.Telefone != null || pessoaFisica.Endereco != null || pessoaFisica.Email != null)
                {
                    Contato contato = new Contato();
                    contato.Telefone = pessoaFisica.Telefone;
                    contato.Endereco = pessoaFisica.Endereco;
                    contato.Email = pessoaFisica.Email;

                    var IdContato = daoContato.Inserir(contato);

                    pessoaFisica.IdContato = IdContato;
                }
                
                pessoaFisica.IdSexo = Convert.ToInt32(pessoaFisica.Sexo);

                pessoaFisicaDao.Cadastrar(pessoaFisica);
            }else{
                TempData["warning"] = "Cpf já cadastrado!";
            }

            return RedirectToAction("Index", "Pessoa");
        }
    }
}
