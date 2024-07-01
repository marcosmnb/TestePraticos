using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questao1
{
    public class ContaBancaria
    {
        private readonly int numeroConta;
        private string titular;
        private double saldo;

        public int NumeroConta
        {
            get { return numeroConta; }
        }

        public string Titular
        {
            get { return titular; }
            set { titular = value; }
        }

        public ContaBancaria(int numeroConta, string titular, double depositoInicial = 0.0)
        {
            this.numeroConta = numeroConta;
            this.titular = titular;
            saldo = depositoInicial;
        }

        public void Depositar(double valor)
        {
            if (valor > 0)
            {
                saldo += valor;
                Console.WriteLine($"Depósito de ${valor} realizado com sucesso. Novo saldo: ${saldo}");
            }
            else
            {
                Console.WriteLine("O valor do depósito deve ser maior que zero.");
            }
        }

        public void Sacar(double valor)
        {
            if (valor > 0)
            {
                if (saldo >= valor)
                {
                    saldo -= valor;
                    saldo -= 3.50; //aplicando a taxa de $3.50 cobrada por saque
                    Console.WriteLine($"Saque de ${valor} realizado com sucesso. Novo saldo: ${saldo}");
                }
                else
                {
                    Console.WriteLine("Saldo insuficiente para realizar o saque.");
                }
            }
            else
            {
                Console.WriteLine("O valor do saque deve ser maior que zero.");
            }
        }

        public void InformacoesConta()
        {
            Console.WriteLine($"Número da conta: {numeroConta}");
            Console.WriteLine($"Titular da conta: {titular}");
            Console.WriteLine($"Saldo atual: ${saldo}");
        }
    }
}
