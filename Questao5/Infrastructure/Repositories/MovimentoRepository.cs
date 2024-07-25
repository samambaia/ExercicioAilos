using Dapper;
using Questao5.Domain.Entities;
using System.Data;

namespace Questao5.Infrastructure.Repositories
{
    public class MovimentoRepository : IMovimentoRepository
    {
        private readonly IDbConnection _context;

        public MovimentoRepository(IDbConnection context)
        {
            _context = context;
        }

        public async Task<string> AddAsync(Movimento movimento)
        {
            var query = "INSERT INTO movimento (idmovimento, idcontacorrente, valor, tipomovimento, datamovimento) " +
                        "VALUES (@IdMovimento, @IdContaCorrente, @Valor, @TipoMovimento, @DataMovimento); ";
            await _context.ExecuteScalarAsync<string>(query, movimento);
            return movimento.IdMovimento;
        }

        public Task<IEnumerable<Movimento>> GetByContaCorrenteIdAsync(string contaCorrenteId)
        {
            var query = "SELECT * FROM movimento WHERE IdContaCorrente = @ContaCorrenteId";
            return _context.QueryAsync<Movimento>(query, new { ContaCorrenteId = contaCorrenteId });
        }

        public async Task<IEnumerable<IdemPotencia>> GetByIdemPotenciaAsync(string chaveIdemPotencia)
        {
            return await _context.QueryAsync<IdemPotencia>(
                "SELECT * FROM idempotencia WHERE chave_idempotencia = @ChaveIdemPotencia", new { ChaveIdemPotencia = chaveIdemPotencia });
        }

        public async Task<Movimento> GetByRequestIdAsync(Guid requestId)
        {
            return await _context.QueryFirstOrDefaultAsync<Movimento>(
                "SELECT * FROM movimento WHERE requestid = @RequestId", new { RequestId = requestId });
        }
    }
}
