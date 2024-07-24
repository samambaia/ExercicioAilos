using MediatR;
using System.Threading.Tasks;

namespace Questao5.Application.Interfaces
{
    public interface IMediatorHandler
    {
        Task<TResponse> SendCommand<TResponse>(IRequest<TResponse> command);
        Task SendCommand<T>(T command) where T : IRequest;
        Task<TResult> SendCommand<T, TResult>(T command) where T : IRequest<TResult>;
        Task<TResult> SendQuery<T, TResult>(T query) where T : IRequest<TResult>;
    }
}
