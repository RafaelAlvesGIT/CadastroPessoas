using CadastroPessoas.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPessoas.Dao
{
    public class ContatoDao
    {
        private IDbConnection Connection;

        public ContatoDao()
        {
            Conexao conexao = new Conexao();
            Connection = conexao.ConexaoBanco();
        }

        public int Inserir(Contato contato)
        {
            var Sql = "INSERT INTO Contato(Email, Endereco, Telefone) " +
                            "OUTPUT INSERTED.[Id]" +
                                  "VALUES (@Email, @Endereco, @Telefone)";
            Connection.Open();
            int newUserId = Connection.QuerySingle<int>(
                    Sql,
                                new
                                {
                                    Email = contato.Email,
                                    Endereco = contato.Endereco,
                                    Telefone = contato.Telefone
                                }
                                );

            Connection.Close();
           
            return newUserId;
        }
    }
}
