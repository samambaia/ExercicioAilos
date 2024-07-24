using Dapper;
using Questao5.Domain.Entities;
using Questao5.Application.Interfaces;
using System.Threading.Tasks;
using System.Data;
using Questao5.Infrastructure.Repositories;

namespace MyProject.Infrastructure.Repositories
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly IDbConnection _dbConnection;

        public ContaCorrenteRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<ContaCorrente> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM ContaCorrente WHERE Id = @Id";
            return _dbConnection.QueryFirstOrDefaultAsync<ContaCorrente>(query, new { Id = id });
        }
    }
}