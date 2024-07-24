using MediatR;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Commands.Requests
{
    public class MovimentacaoCommand : IRequest<MovimentacaoResponse>
    {
        public int IdContaCorrente { get; set; }
        public string TipoMovimento { get; set; }
        public decimal Valor { get; set; }
        public string ChaveIdempotencia { get; set; }
        public Guid RequestId { get; set; }
    }
}
