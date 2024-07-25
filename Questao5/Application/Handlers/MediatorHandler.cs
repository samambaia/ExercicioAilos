using MediatR;
using Questao5.Application.Interfaces;

namespace Questao5.Application.Handlers
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        //public async Task SendCommand<T>(T command) where T : IRequest
        //{
        //    await _mediator.Send(command);
        //}

        //public async Task<TResult> SendCommand<T, TResult>(T command) where T : IRequest<TResult>
        //{
        //    return await _mediator.Send(command);
        //}

        public Task<TResponse> SendCommand<TResponse>(IRequest<TResponse> command)
        {
            return _mediator.Send(command);
        }

        //public async Task<TResult> SendQuery<T, TResult>(T query) where T : IRequest<TResult>
        //{
        //    return await _mediator.Send(query);
        //}
    }
}
