using CadastroRepository.Models;
using CrudBasico.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CrudBasico.Extensions
{
    public static class PessoaEntityExtensions
    {
        public static IEnumerable<PessoaViewModel> ToPessoaViewModels(this IEnumerable<PessoaEntity> pessoaEntities)
        {
            return pessoaEntities.Select(_ =>
            {
                return _.ToPessoaViewModel();
            });
        }

        public static PessoaViewModel ToPessoaViewModel(this PessoaEntity pessoaEntity)
        {
            return new PessoaViewModel()
            {
                Id = pessoaEntity.Id,
                Email = pessoaEntity.Email,
                Nome = pessoaEntity.Nome,
                Endereco = pessoaEntity.Endereco,
                Telefone = pessoaEntity.Telefone,
                CPF = pessoaEntity.CPF,
                Ativo = pessoaEntity.Ativo
            };
        }
    }
}
