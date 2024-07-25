﻿namespace Questao5.Domain.Entities
{
    public class Movimento
    {
        public string IdMovimento { get; set; }
        public string IdContaCorrente { get; set; }
        public decimal Valor { get; set; }
        public string TipoMovimento { get; set; }
        public DateTime DataMovimento { get; set; }
        public Guid RequestId { get; set; }
    }
}
