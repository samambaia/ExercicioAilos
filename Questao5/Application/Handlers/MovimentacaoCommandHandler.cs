using Dapper;
using MediatR;
using MyProject.Infrastructure.Repositories;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyProject.Application.Handlers
{
    public class MovimentacaoCommandHandler : IRequestHandler<MovimentacaoCommand, MovimentacaoResponse>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IMovimentoRepository _movimentoRepository;

        public MovimentacaoCommandHandler(IContaCorrenteRepository contaCorrenteRepository, IMovimentoRepository movimentoRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
            _movimentoRepository = movimentoRepository;
        }

        public async Task<MovimentacaoResponse> Handle(MovimentacaoCommand request, CancellationToken cancellationToken)
        {
            // Verificar se a movimentação já foi processada (Idempotência)
            var existingMovimento = await _movimentoRepository.GetByRequestIdAsync(request.RequestId);
            if (existingMovimento != null)
            {
                return new MovimentacaoResponse { MovimentoId = existingMovimento.IdMovimento };
            }

            // Validar a conta corrente
            var conta = await _contaCorrenteRepository.GetByIdAsync(request.IdContaCorrente);
            if (conta == null)
            {
                throw new Exception("INVALID_ACCOUNT");
            }

            if (!conta.Ativa)
            {
                throw new Exception("INACTIVE_ACCOUNT");
            }

            // Validar o valor
            if (request.Valor <= 0)
            {
                throw new Exception("INVALID_VALUE");
            }

            // Validar o tipo
            if (request.TipoMovimento != "C" && request.TipoMovimento != "D")
            {
                throw new Exception("INVALID_TYPE");
            }

            // Processar a movimentação
            var novoSaldo = request.TipoMovimento == "C" ? conta.Saldo + request.Valor : conta.Saldo - request.Valor;

            var movimento = new Movimento
            {
                IdContaCorrente = request.IdContaCorrente,
                Valor = request.Valor,
                TipoMovimento = request.TipoMovimento,
                DataMovimento = DateTime.Now,
                RequestId = request.RequestId
            };

            var movimentoId = await _movimentoRepository.AddAsync(movimento);

            return new MovimentacaoResponse { MovimentoId = movimentoId };
        }
    }
}
