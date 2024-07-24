using Questao5.Domain.Entities;
using System.Threading.Tasks;

namespace Questao5.Infrastructure.Repositories
{
    public interface IMovimentoRepository
    {
        Task<int> AddAsync(Movimento movimento);
        Task<Movimento> GetByRequestIdAsync(Guid requestId);
        Task<IEnumerable<Movimento>> GetByContaCorrenteIdAsync(int contaCorrenteId);

    }
}