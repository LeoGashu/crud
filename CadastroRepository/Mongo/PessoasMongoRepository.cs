using CadastroRepository.Interfaces;
using CadastroRepository.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CadastroRepository.Mongo
{
    public class PessoasMongoRepository : IPessoaRepository
    {
        protected readonly string connectionString;
        protected readonly string databaseName;
        protected readonly string collectionName;
        protected readonly MongoClient mongoClient;
        protected readonly IMongoCollection<PessoaEntity> pessoas;

        public PessoasMongoRepository(string connectionString, string databaseName, string collectionName)
        {
            this.connectionString = connectionString;
            this.databaseName = databaseName;
            this.collectionName = collectionName;

            this.mongoClient = new MongoClient(this.connectionString);
            var database = this.mongoClient.GetDatabase(this.databaseName);

            var collectionCursor = database.ListCollections(new ListCollectionsOptions { Filter = new BsonDocument("name", this.collectionName) });
            if (!collectionCursor.Any())
            {
                database.CreateCollection(this.collectionName);
            }

            pessoas = database.GetCollection<PessoaEntity>(this.collectionName);
        }

        public async Task<int> DeletePessoaAsync(Guid id)
        {
            var original = await GetPessoaByIdAsync(id);

            original.Ativo = false;

            var result = await pessoas.ReplaceOneAsync(pessoa => pessoa.Id == id, original);

            return (int)result.ModifiedCount;
        }

        public async Task<PessoaEntity> GetPessoaByIdAsync(Guid id)
        {
            return (await pessoas.FindAsync(pessoa => pessoa.Id == id)).FirstOrDefault();
        }

        public async Task<IEnumerable<PessoaEntity>> GetPessoasAsync(bool ativo = true, string nome = null)
        {
            var builder = Builders<PessoaEntity>.Filter;
            var activeFilter = builder.Where(_ => _.Ativo == ativo);
            var filter = activeFilter;

            if (!string.IsNullOrEmpty(nome))
            {
                filter = filter & builder.Text(nome);
            }

            return (await pessoas.FindAsync(filter)).ToList();
        }

        public async Task InsertPessoaAsync(IEnumerable<PessoaEntity> pessoaEntity)
        {
            await pessoas.InsertManyAsync(pessoaEntity);
        }

        public async Task UpdatePessoaAsync(PessoaEntity pessoaEntity)
        {
            await pessoas.ReplaceOneAsync<PessoaEntity>(pessoa => pessoa.Id == pessoaEntity.Id, pessoaEntity);
        }
    }
}
