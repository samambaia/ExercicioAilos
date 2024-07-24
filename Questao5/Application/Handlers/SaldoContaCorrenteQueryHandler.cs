using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Application.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using MyProject.Infrastructure.Repositories;
using Questao5.Infrastructure.Repositories;

namespace Questao5.Application.Handlers
{
    public class SaldoContaCorrenteQueryHandler : IRequestHandler<SaldoContaCorrenteQuery, SaldoContaCorrenteResponse>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IMovimentoRepository _movimentacaoRepository;

        public SaldoContaCorrenteQueryHandler(IContaCorrenteRepository contaCorrenteRepository, IMovimentoRepository movimentacaoRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
            _movimentacaoRepository = movimentacaoRepository;
        }

        public async Task<SaldoContaCorrenteResponse> Handle(SaldoContaCorrenteQuery request, CancellationToken cancellationToken)
        {
            var conta = await _contaCorrenteRepository.GetByIdAsync(request.IdContaCorrente);
            if (conta == null)
            {
                throw new BusinessException("Invalid account.", "INVALID_ACCOUNT");
            }

            if (conta.TipoConta != "CORRENTE")
            {
                throw new BusinessException("Inactive account.", "INACTIVE_ACCOUNT");
            }

            if (!conta.Ativa)
            {
                throw new BusinessException("Inactive account.", "INACTIVE_ACCOUNT");
            }

            var movimentacoes = await _movimentacaoRepository.GetByContaCorrenteIdAsync(request.IdContaCorrente);
            var saldo = movimentacoes.Where(m => m.TipoMovimento == "CREDIT").Sum(m => m.Valor) -
                        movimentacoes.Where(m => m.TipoMovimento == "DEBIT").Sum(m => m.Valor);

            return new SaldoContaCorrenteResponse
            {
                NumeroContaCorrente = conta.Numero,
                NomeTitular = conta.Nome,
                DataHoraConsulta = DateTime.UtcNow,
                SaldoAtual = saldo
            };
        }
    }
}
