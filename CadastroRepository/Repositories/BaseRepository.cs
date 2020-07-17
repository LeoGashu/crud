using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CadastroRepository.Repositories
{
    public class BaseRepository
    {
        protected readonly IDbConnection dbConnection;

        public BaseRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }
    }
}
