using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CadastroPessoas.Models;
using Dapper;

namespace CadastroPessoas.Dao
{
    public class PessoaJuridicaDao
    {
        private IDbConnection Connection;

        public PessoaJuridicaDao()
        {
            Conexao conexao = new Conexao();
            Connection = conexao.ConexaoBanco();
        }
        public IEnumerable<PessoaJuridica> Listar()
        {
            var Sql = ("SELECT " +
                              "* " +
                       "FROM " +
                            "PessoaJuridica "+
                       "LEFT JOIN Contato As Con " +
                             "on PessoaJuridica.IdContato = Con.Id "
                       );

            Connection.Open();
            return Connection.Query<PessoaJuridica, Contato, PessoaJuridica>(Sql,
            map: (pessoaJuridica, contato) =>
            {
                if (contato != null)
                {
                    pessoaJuridica.Endereco = contato.Endereco;
                    pessoaJuridica.Email = contato.Email;
                    pessoaJuridica.Telefone = contato.Telefone;
                }
                    return pessoaJuridica;
            }, splitOn: "Id");
        }

        public IEnumerable<PessoaJuridica> BuscarPorCnpj(string Cnpj)
        {

            var Sql = ("SELECT " +
                              "* " +
                       "FROM " +
                            "PessoaJuridica "+
                       "WHERE "+
                            "Cnpj Like @Cnpj"
                      );

            Connection.Open();

            return Connection.Query<PessoaJuridica>(Sql, new { Cnpj = Cnpj }); 
        }

        public IEnumerable<PessoaJuridica> BuscarDados(PessoaJuridica pessoaJuridica)
        {

            var Sql = ("SELECT " +
                              "* " +
                       "FROM " +
                            "PessoaJuridica "+
                       "LEFT JOIN Contato As Con " +
                             "on PessoaJuridica.IdContato = Con.Id " +
                       "WHERE " +
                            "Cnpj Like @Cnpj or NomeFantasia Like @NomeFantasia"
                      );

            Connection.Open();
             return Connection.Query<PessoaJuridica, Contato, PessoaJuridica>(Sql, 
            map: (pessoaJuridica, contato) =>
            {
                if (contato != null)
                {
                    pessoaJuridica.Endereco = contato.Endereco;
                    pessoaJuridica.Email = contato.Email;
                    pessoaJuridica.Telefone = contato.Telefone;
                }
                    return pessoaJuridica;
            }, new { Cnpj = pessoaJuridica.Cnpj, NomeFantasia = pessoaJuridica.NomeFantasia }, splitOn: "Id");

        }

        public void Cadastrar(PessoaJuridica pessoaJuridica)
        {
            var Sql = "INSERT INTO PessoaJuridica(Cnpj, RazaoSocial, NomeFantasia, IdContato) " +
                                     "VALUES (@Cnpj, @RazaoSocial, @NomeFantasia, @IdContato)";

            Connection.Execute(Sql, pessoaJuridica);
        }
    }
}
