namespace Questao5.Domain.Entities
{
    public class Movimento
    {
        public int IdMovimento { get; set; }
        public int IdContaCorrente { get; set; }
        public decimal Valor { get; set; }
        public string TipoMovimento { get; set; }
        public DateTime DataMovimento { get; set; }

        public Guid RequestId { get; set; }
    }
}
