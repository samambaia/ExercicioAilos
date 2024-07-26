using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;
using Xunit.Abstractions;

namespace Questao5_testes.Integracao
{
    public class API_SaldoContaCorrenteTests
    {
        private readonly HttpClient _client;
        private readonly string idContaCorrente;

        public API_SaldoContaCorrenteTests()
        {
            // Configurar HttpClient para apontar para a sua API
            _client = new HttpClient { BaseAddress = new Uri("https://localhost:44346") };

            // Defina um ID de conta corrente válido para os testes
            idContaCorrente = "B6BAFC09-6967-ED11-A567-055DFA4A16C9";
        }

        [Fact]
        public async Task SaldoEndpointDeveRetornarSucesso()
        {
            // Arrange
            var response = await _client.GetAsync($"/api/ContaCorrente/saldo?idContaCorrente={idContaCorrente}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task SaldoEndpointDeveRetornarBadRequest()
        {
            // Arrange
            var response = await _client.GetAsync($"/api/ContaCorrente/saldo?idContaCorrente=999");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
