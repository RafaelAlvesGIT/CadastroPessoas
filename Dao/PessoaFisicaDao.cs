using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CadastroPessoas.Models;
using Dapper;

namespace CadastroPessoas.Dao
{
    public class PessoaFisicaDao
    {
        private IDbConnection Connection;

        public PessoaFisicaDao()
        {
            Conexao conexao = new Conexao();
            Connection = conexao.ConexaoBanco();
        }
        public IEnumerable<PessoaFisica> Listar()
        {
            var Sql = ("SELECT " +
                              "* " +
                       "FROM " +
                            "PessoaFisica " +
                       "LEFT JOIN Contato As Con " +
                             "on PessoaFisica.IdContato = Con.Id " 
                       );

            Connection.Open();
            return Connection.Query<PessoaFisica, Contato, PessoaFisica>(Sql,
            map: (pessoaFisica, contato) =>
            {
                if (contato != null)
                {
                    pessoaFisica.Endereco = contato.Endereco;
                    pessoaFisica.Email = contato.Email;
                    pessoaFisica.Telefone = contato.Telefone;
                }
                return pessoaFisica;
            }, splitOn: "Id");
        }
        
        public IEnumerable<PessoaFisica> BuscarPorCpf(string Cpf)
        {

            var Sql = ("SELECT " +
                              "* " +
                       "FROM " +
                            "PessoaFisica "+
                       "WHERE "+
                            "Cpf Like @Cpf"
                      );

            Connection.Open();

            return Connection.Query<PessoaFisica>(Sql, new { Cpf = Cpf }); 
        }

        public IEnumerable<PessoaFisica> BuscarDados(PessoaFisica pessoaFisica)
        {

            var Sql = ("SELECT " +
                              "* " +
                       "FROM " +
                       "LEFT JOIN Contato As Con " +
                             "on PessoaFisica.IdContato = Con.Id "
                       );

            Connection.Open();
            return Connection.Query<PessoaFisica, Contato, PessoaFisica>(Sql,
            map: (pessoaFisica, contato) =>
            {
                if (contato != null)
                {
                    pessoaFisica.Endereco = contato.Endereco;
                    pessoaFisica.Email = contato.Email;
                    pessoaFisica.Telefone = contato.Telefone;
                }
                return pessoaFisica;
            }, new { Cpf = pessoaFisica.Cpf, Rg = pessoaFisica.Rg, Nome = pessoaFisica.Nome }, splitOn: "Id");
        }

        public void Cadastrar(PessoaFisica pessoaFisica)
        {
            var Sql = "INSERT INTO PessoaFisica(Nome, Cpf, DataNascimento, Rg, IdSexo, IdContato) " +
                                     "VALUES (@Nome, @Cpf, @DataNascimento, @Rg, @IdSexo, @IdContato)";

            Connection.Execute(Sql, pessoaFisica);
        }
    }
}
