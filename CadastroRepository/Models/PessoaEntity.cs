using System;

namespace CadastroRepository.Models
{
    public class PessoaEntity
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }
    }
}