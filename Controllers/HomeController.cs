using CadastroPessoas.Dao;
using CadastroPessoas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPessoas.Controllers
{
    public class HomeController : Controller
    {
        public IEnumerable<Sexo> BuscarSexo()
        {
            SexoDao DaoSexo = new SexoDao();
            IEnumerable<Sexo> ListaSexo = DaoSexo.Listar();
            return ListaSexo;
        }
    }
}
