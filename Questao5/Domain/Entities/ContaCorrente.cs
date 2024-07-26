namespace Questao5.Domain.Entities
{
    public class ContaCorrente
    {
        public string IdContaCorrente { get; set; }
        public int Numero { get; set; }
        public string Nome { get; set; }
        public decimal Saldo { get; set; }
        public string TipoConta { get; set; }
        public bool Ativo { get; set; }
    }
}

