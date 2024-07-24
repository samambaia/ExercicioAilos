namespace Questao5.Application.Queries.Responses
{
    public class SaldoResponse
    {
        public string NumeroContaCorrente { get; set; }
        public string NomeTitular { get; set; }
        public decimal SaldoAtual { get; set; }
        public DateTime DataHoraResposta { get; set; }
    }
}
