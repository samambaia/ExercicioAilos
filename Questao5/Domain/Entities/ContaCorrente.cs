namespace Questao5.Domain.Entities
{
    public class ContaCorrente
    {
        public int IdContaCorrente { get; set; }
        public string Numero { get; set; }
        public string Nome { get; set; }
        public decimal Saldo { get; set; }
        public string TipoConta { get; set; }
        public bool Ativa { get; set; }
    }
}

