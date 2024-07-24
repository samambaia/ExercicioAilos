using Microsoft.Data.Sqlite;
using System.Data;

namespace Questao5.Infrastructure.Database
{
    public class DatabaseContext
    {
        private readonly string _connectionString;

        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection() => new SqliteConnection(_connectionString);
    }
}
