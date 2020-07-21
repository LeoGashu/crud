using CadastroRepository.Models;
using CadastroRepository.Interfaces;
using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CadastroRepository.Sqlite
{
    public class PessoaRepository : BaseRepository, IPessoaRepository
    {
        protected readonly string BaseQuery = "SELECT Id, Email, Nome, Endereco, Telefone, CPF, Ativo From Pessoa ";
        public PessoaRepository(string connectionString) : base(connectionString)
        {

        }

        public async Task<int> DeletePessoaAsync(Guid id)
        {
            using (var dbConnection = GetDbConnection())
            {
                return await dbConnection.ExecuteAsync("UPDATE Pessoa SET Ativo = 0 WHERE Id = @Id", new { Id = id.ToString() });
            }
        }

        public async Task<PessoaEntity> GetPessoaByIdAsync(Guid id)
        {
            using (var dbConnection = GetDbConnection())
            {
                return await dbConnection.QueryFirstOrDefaultAsync<PessoaEntity>($"{this.BaseQuery} WHERE Id = @Id", new { Id = id.ToString() });
            }
        }

        public async Task<IEnumerable<PessoaEntity>> GetPessoasAsync(bool ativo = true, string nome = null)
        {
            string query = $"{this.BaseQuery} WHERE Ativo = @Ativo";
            string nomeParam = nome;

            if (!string.IsNullOrEmpty(nomeParam))
            {
                nomeParam = $"%{nomeParam}%";
                query += " AND (@Nome IS NULL OR Nome LIKE @Nome)";
            }

            object param = new
            {
                Ativo = ativo,
                Nome = nomeParam
            };

            using (var dbConnection = GetDbConnection())
            {
                return await dbConnection.QueryAsync<PessoaEntity>(query, param);
            }
        }

        public async Task InsertPessoaAsync(IEnumerable<PessoaEntity> pessoaEntity)
        {
            string baseQuery = "INSERT INTO Pessoa (Id, Email, Nome, Endereco, Telefone, CPF, Ativo) VALUES (@Id, @Email, @Nome, @Endereco, @Telefone, @CPF, @Ativo)";
            List<CommandParamKeyPair> queries = new List<CommandParamKeyPair>();
            foreach (var pessoa in pessoaEntity)
            {
                if (pessoa.Id == null || pessoa.Id == Guid.Empty)
                {
                    pessoa.Id = Guid.NewGuid();
                }

                queries.Add(new CommandParamKeyPair(baseQuery, new {
                    Id = pessoa.Id.ToString(),
                    pessoa.Email,
                    pessoa.Nome,
                    pessoa.Endereco,
                    pessoa.Telefone,
                    pessoa.CPF,
                    pessoa.Ativo
                }));
            }

            using (var dbConnection = GetDbConnection())
            {
                foreach (var command in queries)
                {
                    await dbConnection.ExecuteAsync(command.Query, command.Param);
                }
            }
        }

        public async Task UpdatePessoaAsync(PessoaEntity pessoaEntity)
        {
            using (var dbConnection = GetDbConnection())
            {
                await dbConnection.UpdateAsync<PessoaEntity>(pessoaEntity);
            }
        }

        private struct CommandParamKeyPair
        {
            public string Query;
            public object Param;
            public CommandParamKeyPair(string query, object param)
            {
                this.Query = query;
                this.Param = param;
            }
        }
    }
}
