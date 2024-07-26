using NSubstitute;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Repositories;
using Volo.Abp;
using Xunit;

namespace Questao5_testes.Unitarios
{
    public class SaldoContaCorrenteCommandHandlerTests
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IMovimentoRepository _movimentacaoRepository;
        private readonly SaldoContaCorrenteCommandHandler _handler;

        public SaldoContaCorrenteCommandHandlerTests()
        {
            _contaCorrenteRepository = Substitute.For<IContaCorrenteRepository>();
            _movimentacaoRepository = Substitute.For<IMovimentoRepository>();
            _handler = new SaldoContaCorrenteCommandHandler(_contaCorrenteRepository, _movimentacaoRepository);
        }

        [Fact]
        public async Task Handle_RetornaSaldoCorreto()
        {
            // Arrange
            var contaId = Guid.NewGuid().ToString();
            var conta = new ContaCorrente { IdContaCorrente = contaId, Numero = 12345, Nome = "Bob Marley", Ativo = true };
            var movimentacoes = new List<Movimento>
        {
            new Movimento { TipoMovimento = "C", Valor = 200m },
            new Movimento { TipoMovimento = "D", Valor = 50m }
        };

            _contaCorrenteRepository.GetByIdAsync(contaId).Returns(conta);
            _movimentacaoRepository.GetByContaCorrenteIdAsync(contaId).Returns(movimentacoes);

            var request = new SaldoContaCorrenteQuery { IdContaCorrente = contaId };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(conta.Numero, result.NumeroContaCorrente);
            Assert.Equal(conta.Nome, result.NomeTitular);
            Assert.Equal(150m, result.SaldoAtual); // 200 - 50 = 150
        }

        [Fact]
        public async Task Handle_RetornaBusinessExceptionInvalidAccount()
        {
            // Arrange
            var contaId = Guid.NewGuid().ToString();
            _contaCorrenteRepository.GetByIdAsync(contaId).Returns((ContaCorrente)null);

            var request = new SaldoContaCorrenteQuery { IdContaCorrente = contaId };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Equal("INVALID_ACCOUNT", exception.Message);
        }

        [Fact]
        public async Task Handle_RetornaBusinessExceptionInactiveAccount()
        {
            // Arrange
            var contaId = Guid.NewGuid().ToString();
            var conta = new ContaCorrente { IdContaCorrente = contaId, Numero = 12345, Nome = "Marcus Garvey", Ativo = false };

            _contaCorrenteRepository.GetByIdAsync(contaId).Returns(conta);

            var request = new SaldoContaCorrenteQuery { IdContaCorrente = contaId };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Equal("INACTIVE_ACCOUNT", exception.Message);
        }
    }
}