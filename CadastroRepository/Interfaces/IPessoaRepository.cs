using CadastroRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CadastroRepository.Interfaces
{
    public interface IPessoaRepository
    {
        Task<IEnumerable<PessoaEntity>> GetPessoasAsync(bool ativo = true, string nome = null);
        Task<PessoaEntity> GetPessoaByIdAsync(Guid id);
        Task InsertPessoaAsync(IEnumerable<PessoaEntity> pessoaEntity);
        Task UpdatePessoaAsync(PessoaEntity pessoaEntity);
    }
}
