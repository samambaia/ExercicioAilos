using Dapper;
using Questao5.Domain.Entities;
using System.Data;

namespace Questao5.Infrastructure.Repositories
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly IDbConnection _dbConnection;

        public ContaCorrenteRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<ContaCorrente> GetByIdAsync(string id)
        {
            var query = "SELECT * FROM ContaCorrente WHERE IdContaCorrente = @Id";
            return _dbConnection.QueryFirstOrDefaultAsync<ContaCorrente>(query, new { Id = id });
        }
    }
}