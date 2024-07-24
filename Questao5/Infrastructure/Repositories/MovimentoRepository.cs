using Dapper;
using Questao5.Domain.Entities;
using Questao5.Application.Interfaces;
using Questao5.Infrastructure.Database;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Questao5.Infrastructure.Repositories;

namespace MyProject.Infrastructure.Repositories
{
    public class MovimentoRepository : IMovimentoRepository
    {
        private readonly DatabaseContext _context;

        public MovimentoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Movimento movimento)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "INSERT INTO movimento (idcontacorrente, valor, tipomovimento, datamovimento) " +
                            "VALUES (@ContaId, @Valor, @Tipo, @DataMovimento); " +
                            "SELECT last_insert_rowid();";
                return await connection.ExecuteScalarAsync<int>(query, movimento);
            }
        }

        public Task<IEnumerable<Movimento>> GetByContaCorrenteIdAsync(int contaCorrenteId)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "SELECT * FROM Movimentos WHERE IdContaCorrente = @ContaCorrenteId";
                return connection.QueryAsync<Movimento>(query, new { ContaCorrenteId = contaCorrenteId });
            }
        }

        public async Task<Movimento> GetByRequestIdAsync(Guid requestId)
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Movimento>(
                    "SELECT * FROM movimento WHERE requestid = @RequestId", new { RequestId = requestId });
            }
        }
    }
}
