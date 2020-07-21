using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CadastroRepository.Models
{
    public class PessoaEntity
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string CPF { get; set; }
        public bool Ativo { get; set; }
    }
}