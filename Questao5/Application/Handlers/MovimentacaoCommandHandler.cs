using MediatR;
using Newtonsoft.Json;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Repositories;

namespace Questao5.Application.Handlers
{
    public class MovimentacaoCommandHandler : IRequestHandler<MovimentacaoCommand, MovimentacaoResponse>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IMovimentoRepository _movimentoRepository;
        private readonly IIdempotenciaRepository _idempotenciaRepository;

        public MovimentacaoCommandHandler(IContaCorrenteRepository contaCorrenteRepository, IMovimentoRepository movimentoRepository, IIdempotenciaRepository idempotenciaRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
            _movimentoRepository = movimentoRepository;
            _idempotenciaRepository = idempotenciaRepository;
        }

        public async Task<MovimentacaoResponse> Handle(MovimentacaoCommand request, CancellationToken cancellationToken)
        {
            // Gera um novo GUID para a chave de idempotencia
            var newGuidIdemPotencia = Guid.NewGuid().ToString();

            // Verifica se a chave de idempotência já foi utilizada
            var idempotencia = await _idempotenciaRepository.GetByIdempotenciaKeyAsync(newGuidIdemPotencia);
            if (idempotencia != null)
            {
                // Retorna o resultado armazenado
                return JsonConvert.DeserializeObject<MovimentacaoResponse>(idempotencia.Resultado);
            }

            // Valida a conta corrente
            var conta = await _contaCorrenteRepository.GetByIdAsync(request.IdContaCorrente);
            if (conta == null)
            {
                throw new Exception("INVALID_ACCOUNT");
            }

            // Valida se a conta está ativa
            if (!conta.Ativo)
            {
                throw new Exception("INACTIVE_ACCOUNT");
            }

            // Valida o valor
            if (request.Valor <= 0)
            {
                throw new Exception("INVALID_VALUE");
            }

            // Valida o tipo
            if (request.TipoMovimento != "C" && request.TipoMovimento != "D")
            {
                throw new Exception("INVALID_TYPE");
            }

            // Processa a movimentação
            var novoSaldo = request.TipoMovimento == "C" ? conta.Saldo + request.Valor : conta.Saldo - request.Valor;

            var movimento = new Movimento
            {
                IdMovimento = Guid.NewGuid().ToString(),
                IdContaCorrente = request.IdContaCorrente,
                Valor = request.Valor,
                TipoMovimento = request.TipoMovimento,
                DataMovimento = DateTime.Now,
                RequestId = request.RequestId
            };

            var movimentoId = await _movimentoRepository.AddAsync(movimento);

            var response = new MovimentacaoResponse { MovimentoId = movimentoId };

            // Armazena a requisicao e o resultado
            var idempotenciaRecord = new IdemPotencia
            {
                Chave_idempotencia = newGuidIdemPotencia,
                Requisicao = JsonConvert.SerializeObject(request),
                Resultado = JsonConvert.SerializeObject(response)
            };

            await _idempotenciaRepository.AddAsync(idempotenciaRecord);

            return response;
        }
    }
}
