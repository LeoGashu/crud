using CadastroRepository.Models;
using CadastroRepository.Interfaces;
using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CadastroRepository.Repositories
{
    public class PessoaRepository : BaseRepository, IPessoaRepository
    {
        protected readonly string BaseQuery = "SELECT Id, Nome, Telefone, CPF From Pessoa (NOLOCK)";
        public PessoaRepository(IDbConnection dbConnection) : base(dbConnection)
        {

        }

        public async Task<PessoaEntity> GetPessoaByIdAsync(Guid id)
        {
            return await dbConnection.QueryFirstOrDefaultAsync<PessoaEntity>($"{this.BaseQuery} WHERE Id = @Id", new { Id = id });
        }

        public async Task<IEnumerable<PessoaEntity>> GetPessoasAsync(bool ativo = true, string nome = null)
        {
            string query = $"{this.BaseQuery} WHERE Ativo = @ativo";
            string nomeParam = nome;

            if (!string.IsNullOrEmpty(nomeParam))
            {
                nomeParam = $"%{nomeParam}%";
                query += " AND (@nome IS NULL OR Nome LIKE @Nome)";
            }

            object param = new
            {
                Ativo = ativo,
                Nome = nomeParam
            };

            return await dbConnection.QueryAsync<PessoaEntity>(query, param);

            throw new NotImplementedException();
        }

        public async Task InsertPessoaAsync(IEnumerable<PessoaEntity> pessoaEntity)
        {
            foreach (var pessoa in pessoaEntity)
            {
                if (pessoa.Id == null || pessoa.Id == Guid.Empty)
                {
                    pessoa.Id = Guid.NewGuid();
                }
            }

            await dbConnection.InsertAsync<PessoaEntity>(pessoaEntity);
        }

        public async Task UpdatePessoaAsync(PessoaEntity pessoaEntity)
        {
            await dbConnection.UpdateAsync<PessoaEntity>(pessoaEntity);
        }
    }
}
