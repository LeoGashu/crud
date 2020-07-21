using Dapper;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CadastroRepository.Sqlite
{
    public class BaseRepository
    {
        protected readonly string connectionString;

        public BaseRepository(string connectionString)
        {
            this.connectionString = connectionString;

            using (var dbConnection = GetDbConnection())
            {
                dbConnection.Open();

                // Create a Product table
                dbConnection.Execute(@"
                    CREATE TABLE IF NOT EXISTS [Pessoa] (
                        [Id] UID NOT NULL PRIMARY KEY,
                        [Email] VARCHAR(50) NOT NULL,
                        [Nome] VARCHAR(128) NULL,
                        [Endereco] VARCHAR(200) NULL,
                        [Telefone] VARCHAR(30) NULL,
                        [CPF] VARCHAR(11) NULL,
                        [Ativo] INTEGER NOT NULL DEFAULT 1
                    )");

                dbConnection.Close();
            }
        }

        protected IDbConnection GetDbConnection()
        {
            return new SqliteConnection(this.connectionString);
        }
    }
}
