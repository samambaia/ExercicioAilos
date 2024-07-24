using System.Globalization;
using System;

namespace Questao1
{
    public class ContaBancaria
    {
        public int NumeroConta { get; }
        public string NomeTitular { get; set; }
        public double Saldo { get; set; }
        private const double TaxaSaque = 3.50;

        public ContaBancaria(int numeroConta, string nomeTitular, double depositoInicial = 0.0)
        {
            NumeroConta = numeroConta;
            NomeTitular = nomeTitular;
            Saldo = depositoInicial;
        }

        public void Deposito(double valor)
        {
            if (valor > 0)
            {
                Saldo += valor;
            }
            else
            {
                Console.WriteLine("Valor de depósito deve ser positivo.");
            }
        }

        public void Saque(double valor)
        {
            if (valor > 0)
            {
                Saldo -= (valor + TaxaSaque);
            }
            else
            {
                Console.WriteLine("Valor de saque deve ser positivo.");
            }
        }

        public void MostrarDadosConta()
        {
            string saldoFormatado = Saldo.ToString("C", CultureInfo.GetCultureInfo("en-US"));
            Console.WriteLine($"Conta: {NumeroConta}, Titular: {NomeTitular}, Saldo: {saldoFormatado}");
        }
    }
}
