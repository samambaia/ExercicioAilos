using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Repositories
{
    public interface IMovimentoRepository
    {
        Task<string> AddAsync(Movimento movimento);
        Task<Movimento> GetByRequestIdAsync(Guid requestId);
        Task<IEnumerable<Movimento>> GetByContaCorrenteIdAsync(string contaCorrenteId);
        Task<IEnumerable<IdemPotencia>> GetByIdemPotenciaAsync(string chaveIdemPotencia);
    }
}