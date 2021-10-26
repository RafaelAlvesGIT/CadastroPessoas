using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using CadastroPessoas.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPessoas.Dao
{
    public class SexoDao
    {
        private IDbConnection Connection;

        public SexoDao()
        {
            Conexao conexao = new Conexao();
            Connection = conexao.ConexaoBanco();
        }

        public IEnumerable<Sexo> Listar()
        {
            var Sql = ("SELECT " +
                              "* " +
                       "FROM " +
                            "Sexo "
                      );

            Connection.Open();
            return Connection.Query<Sexo>(Sql);
        }

    }
}
