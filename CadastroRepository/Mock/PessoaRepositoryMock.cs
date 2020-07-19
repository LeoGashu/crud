using CadastroRepository.Interfaces;
using CadastroRepository.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroRepository.Mock
{
    public class PessoaRepositoryMock : IPessoaRepository
    {
        private List<PessoaEntity> pessoaEntities = new List<PessoaEntity>();

        public PessoaRepositoryMock()
        {
            pessoaEntities = new List<PessoaEntity>()
            {
                new PessoaEntity()
                {
                    Id = Guid.NewGuid(),
                    Email = "leogashu@gmail.com",
                    Nome = "Leonardo Yuiti Gashu",
                    CPF = "42277674826",
                    Telefone = @"+5511992628781",
                    Endereco = "Rua do Arraial, 52 - APT 26",
                    Ativo = true
                },
                new PessoaEntity()
                {
                    Id = Guid.NewGuid(),
                    Email = "fulano@email.com",
                    Nome = "Fulano",
                    CPF = "94364447021",
                    Telefone = @"+5511999999999",
                    Endereco = "Rua dois, 123 - Algum lugar",
                    Ativo = true
                },
                new PessoaEntity()
                {
                    Id = Guid.NewGuid(),
                    Email = "nobody@email.com",
                    Nome = "Ninguém",
                    CPF = "67719627091",
                    Telefone = @"+5511999999991",
                    Endereco = "Rua dois, 2 - Lugar nenhum",
                    Ativo = false
                },
            };
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<PessoaEntity> GetPessoaByIdAsync(Guid id) => pessoaEntities.FirstOrDefault(_ => _.Id == id);
        public async Task<IEnumerable<PessoaEntity>> GetPessoasAsync(bool ativo = true, string nome = null) => pessoaEntities.Where(_ => _.Ativo == ativo && (string.IsNullOrEmpty(nome) || _.Nome == nome));

        public async Task InsertPessoaAsync(IEnumerable<PessoaEntity> pessoaEntity)
        {
            foreach (var pessoa in pessoaEntity)
            {
                if (pessoa.Id == null || pessoa.Id == Guid.Empty)
                {
                    pessoa.Id = Guid.NewGuid();
                }

                if (pessoaEntities.FirstOrDefault(_ => _.Id == pessoa.Id) != null)
                {
                    throw new Exception($"{pessoa.Nome} de Id {pessoa.Id} já inserida");
                }
            }

            pessoaEntities.AddRange(pessoaEntity);
        }

        public async Task UpdatePessoaAsync(PessoaEntity pessoaEntity)
        {
            if (pessoaEntity == null)
                throw new ArgumentNullException();

            var originalEntity = pessoaEntities.FirstOrDefault(_ => _.Id == pessoaEntity.Id);

            if (originalEntity == null)
            {
                await InsertPessoaAsync(new List<PessoaEntity>() { pessoaEntity });
                return;
            }

            pessoaEntities.RemoveAll(_ => _.Id == pessoaEntity.Id);

            pessoaEntities.Add(pessoaEntity);
        }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    }
}
