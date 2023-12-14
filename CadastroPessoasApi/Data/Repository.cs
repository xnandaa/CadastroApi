﻿using CadastroPessoasApi.ViewModel;
using Dapper;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace CadastroPessoasApi.Data
{
    public class Repository
    {
        string conexao = @"Server=(localdb)\mssqllocaldb;Database=CADASTROPESSOAS;Trusted_Connection=True;MultipleActiveResultSets=True";

        public IEnumerable<PessoaViewModel> GetAll()
        {
            string query = "SELECT TOP 1000 * FROM PESSOAS";
            using (SqlConnection con = new SqlConnection(conexao))
            {
                return con.Query<PessoaViewModel>(query);
            }




        }

        public PessoaViewModel GetById(int pessoaId)
        {
            string query = "SELECT * FROM PESSOAS WHERE pessoaId = @pessoaId";
            using (SqlConnection con = new SqlConnection(conexao))
            {
                return con.QueryFirstOrDefault<PessoaViewModel>(query, new { pessoaId = pessoaId });
            }

        }
        public PessoaViewModel GetByprimeiroNome(string primeiroNome)
        {
            string query = "SELECT * FROM PESSOAS WHERE primeiroNome = @primeiroNome";
            using (SqlConnection con = new SqlConnection(conexao))
            {
                return con.QueryFirstOrDefault<PessoaViewModel>(query, new { primeiroNome = primeiroNome });
            }
        }
        public void Update(int pessoaId, string primeiroNome)
        {
            string query = "UPDATE PESSOAS SET primeiroNome = @primeiroNome WHERE pessoaId = pessoaId";
            using (SqlConnection con = new SqlConnection(conexao))
            {
                con.Execute(query, new { pessoaId = pessoaId, primeiroNome = primeiroNome });
            }


        }
        public string Create(PessoaViewModel pessoa)
        {
            try
            {
                string query = @"INSERT INTO PESSOAS(primeiroNome , nomeMeio, ultimoNome , CPF) VALUES(@primeiroNome,@nomeMeio,@ultimoNome, @cpf)";
                using (SqlConnection con = new SqlConnection(conexao))
                {
                    con.Execute(query, new
                    {
                        primeiroNome = pessoa.primeiroNome,
                        nomeMeio = pessoa.nomeMeio,
                        cpf = pessoa.CPF,
                        ultimoNome = pessoa.ultimoNome,
                    });

                }
                return null;
            }

            catch (Exception ex)
            {
                throw ex;

            }
        }

    }




}
