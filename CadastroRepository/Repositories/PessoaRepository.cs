using CadastroRepository.Models;
using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CadastroRepository.Repositories
{
    public interface IPessoaRepository
    {
        Task<PessoaEntity> GetPessoaById(Guid id);
        Task InsertPessoaAsync(IEnumerable<PessoaEntity> pessoaEntity);
        Task<bool> UpdatePessoaAsync(PessoaEntity pessoaEntity);
    }

    public class PessoaRepository : BaseRepository, IPessoaRepository
    {
        protected readonly string BaseQuery = "SELECT Id, Nome, Telefone, CPF From Pessoa (NOLOCK)";
        public PessoaRepository(IDbConnection dbConnection) : base(dbConnection)
        {

        }

        public async Task<PessoaEntity> GetPessoaById(Guid id)
        {
            return await dbConnection.QueryFirstOrDefaultAsync<PessoaEntity>($"{this.BaseQuery} WHERE Id = @Id", new { Id = id });
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

        public async Task<bool> UpdatePessoaAsync(PessoaEntity pessoaEntity)
        {
            return await dbConnection.UpdateAsync<PessoaEntity>(pessoaEntity);
        }
    }
}
