namespace Questao5.Application.Queries.Responses
{
    public class SaldoContaCorrenteResponse
    {
        public string NumeroContaCorrente { get; set; }
        public string NomeTitular { get; set; }
        public DateTime DataHoraConsulta { get; set; }
        public decimal SaldoAtual { get; set; }
    }
}
