// See https://aka.ms/new-console-template for more information
using Questao1;

internal class Program
{
    private static void Main(string[] args)
    {
        int numeroConta = NumeroDaConta();
        string nomeTitular = Titular();
        double depositoInicial = DepositoInicial();

        ContaBancaria conta = new(numeroConta, nomeTitular, depositoInicial);

        Console.WriteLine("\nConta criada com sucesso!");
        conta.InformacoesConta();

        //não vou validar as operações para não adicionar mais complexidade em uma aplicação console
        Console.WriteLine("\nEntre com um valor para depósito:");
        double valorDeposito = double.Parse(Console.ReadLine());
        conta.Depositar(valorDeposito);

        Console.WriteLine("\nInformações atualizadas da conta:");
        conta.InformacoesConta();

        Console.WriteLine("\nEntre com um valor para saque:");
        double valorSaque = double.Parse(Console.ReadLine());
        conta.Sacar(valorSaque);

        Console.WriteLine("\nInformações atualizadas da conta após o saque:");
        conta.InformacoesConta();
        Console.ReadLine();

        static int NumeroDaConta()
        {
            int numeroConta;
            while (true)
            {
                Console.WriteLine("Entre com o número da conta:");
                string entrada = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(entrada) && int.TryParse(entrada, out numeroConta))
                {
                    break;
                }

                Console.WriteLine("Erro: A entrada deve ser um número inteiro válido e não pode ser nula ou vazia. Por favor, tente novamente.");
            }

            return numeroConta;
        }

        static string Titular()
        {
            string nomeTitular;
            while (true)
            {
                Console.WriteLine("Entre com o nome do titular da conta:");
                nomeTitular = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(nomeTitular))
                {
                    break;
                }

                Console.WriteLine("Erro: Por favor, entre com o nome do titular da conta.");
            }

            return nomeTitular;
        }

        static double DepositoInicial()
        {
            double depositoInicial = 0.0;
            string resposta;
            while (true)
            {
                Console.WriteLine("Deseja realizar um depósito inicial? (s/n)");
                resposta = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(resposta) && (resposta.Equals("s", StringComparison.OrdinalIgnoreCase) || resposta.Equals("n", StringComparison.OrdinalIgnoreCase)))
                {
                    break;
                }

                Console.WriteLine("Erro: Por favor, digite s para sim e n para não, para um depósito inicial");
            }

            
            if (resposta.ToLower() == "s")
            {
                while (true)
                {
                    Console.WriteLine("Entre com o valor do depósito inicial:");
                    string entrada = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(entrada) && double.TryParse(entrada, out depositoInicial))
                    {
                        break;
                    }

                    Console.WriteLine("Erro: A entrada deve ser um número válido e não pode ser nula ou vazia. Por favor, tente novamente.");
                }
            }

            return depositoInicial;
        }
    }
}

#region Cadastro

#endregion
