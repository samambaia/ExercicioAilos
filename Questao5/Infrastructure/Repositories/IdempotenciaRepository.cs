using Dapper;
using Questao5.Domain.Entities;
using System.Data;

namespace Questao5.Infrastructure.Repositories
{
    public class IdempotenciaRepository : IIdempotenciaRepository
    {
        private readonly IDbConnection _dbConnection;
        public IdempotenciaRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task AddAsync(IdemPotencia idempotencia)
        {
            var query = "INSERT INTO IDEMPOTENCIA (chave_idempotencia, requisicao, resultado) VALUES (@Chave_idempotencia, @Requisicao, @Resultado)";
            await _dbConnection.ExecuteAsync(query, new
            {
                Chave_idempotencia = idempotencia.Chave_idempotencia,
                Requisicao = idempotencia.Requisicao,
                Resultado = idempotencia.Resultado
            });
        }

        public async Task<IdemPotencia> GetByIdempotenciaKeyAsync(string idempotenciaKey)
        {
            var query = "SELECT * FROM IDEMPOTENCIA WHERE chave_idempotencia = @Chave_Idempotencia";
            return await _dbConnection.QueryFirstOrDefaultAsync<IdemPotencia>(query, new { Chave_Idempotencia = idempotenciaKey });
        }
    }
}
