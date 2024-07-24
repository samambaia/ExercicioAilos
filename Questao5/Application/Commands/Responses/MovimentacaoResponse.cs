namespace Questao5.Application.Commands.Responses
{
    public class MovimentacaoResponse
    {
        public int MovimentoId { get; set; }
        public string NumeroContaCorrente { get; set; }
        public string NomeTitular { get; set; }
        public decimal SaldoAtual { get; set; }
        public DateTime DataMovimentacao { get; set; }
    }
}
