using Questao5.Domain.Entities;
using System.Threading.Tasks;

namespace Questao5.Infrastructure.Repositories
{
    public interface IContaCorrenteRepository
    {
        Task<ContaCorrente> GetByIdAsync(int id);
    }
}