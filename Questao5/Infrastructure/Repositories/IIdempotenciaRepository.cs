using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Repositories
{
    public interface IIdempotenciaRepository
    {
        Task<IdemPotencia> GetByIdempotenciaKeyAsync(string idempotenciaKey);
        Task AddAsync(IdemPotencia idempotencia);
    }
}
