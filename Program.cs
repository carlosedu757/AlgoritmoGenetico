using AlgoritmoGenetico.functions;
using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AlgoritmoGenetico
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<int> population = new List<int>();

            while(population.Count < 4)
            {
                // ESCOLHER POPULAÇÃO ALEATORIA
                // int numAleatorio = new Random().Next(-10, 11);

                // if(!population.Exists(x => x == numAleatorio))
                //     population.Add(numAleatorio);

                population.Add(population.Count - 1);
            }


            int i = 0;
            do
            {
                List<(int, string)> aptos = new List<(int, string)>();
                Console.WriteLine("-----------------------------------------------------");
                if (i == 0)
                    Console.WriteLine("Populacao Inicial");
                else
                    Console.WriteLine(i + "° GERACAO");

                Console.WriteLine("Cromossomo | x | f(x) ");

                for (int k = 1; k <= 4; k++)
                {

                    bool negativo = population[k - 1] < 0;
                    int valorAbsoluto = Math.Abs(population[k - 1]);

                    string numeroBinario = Convert.ToString(valorAbsoluto, 2).PadLeft(5, '0');

                    int fx = ProgramTRA.FuncaoQuadratica(population[k - 1]);

                    if (negativo)
                    {
                        // CONVERSÃO INTEIRO NEGATIVO -> BINARIO
                        numeroBinario = Convert.ToString(Convert.ToInt32(numeroBinario, 2) - 1, 2).PadLeft(5, '0');
                        numeroBinario = new string(numeroBinario.Select(bit => bit == '0' ? '1' : '0').ToArray());
                    }
                    aptos.Add((fx, numeroBinario));

                    Console.WriteLine(k + " | " + numeroBinario + " | " + population[k - 1] + " | " + fx);
                }

                Console.WriteLine("\nCRIAÇÃO DE UMA NOVA GERAÇÃO...\n");

                List<(int, string)> maisAptos = ProgramTRA.SelecaoTorneio(aptos); // SELECAO POR TORNEIO

                Console.WriteLine("CROSSOVER\n");

                List<string> crossovers = ProgramTRA.Crossover(maisAptos);

                Console.WriteLine("\nMUTACAO\n");

                List<string> mutacoes = ProgramTRA.Mutacoes(crossovers);

                Console.WriteLine("1 - " + crossovers[0] + " ---> " + mutacoes[0]);
                Console.WriteLine("2 - " + crossovers[1] + " ---> " + mutacoes[1]);
                Console.WriteLine("3 - " + crossovers[2] + " ---> " + mutacoes[2]);
                Console.WriteLine("4 - " + crossovers[3] + " ---> " + mutacoes[3]);

                population.Clear(); // Limpar lista de população para preencher com nova geração

                //preenchimento de nova geração
                foreach (string mutacao in mutacoes)
                {
                    int numerox;
                    // CONVERSÃO BINARIO -> INTEIRO NEGATIVO
                    if (mutacao[0] == '1')
                    {
                        string novoBinario = new string(mutacao.Select(bit => bit == '0' ? '1' : '0').ToArray());
                        novoBinario = Convert.ToString(Convert.ToInt32(novoBinario, 2) + 1, 2).PadLeft(5, '0');
                        numerox = -Convert.ToInt32(novoBinario, 2);
                    }
                    else
                        numerox = Convert.ToInt32(mutacao, 2);
                    population.Add(numerox);
                }

                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine("\n");
                i++;
            }while(i <= 5);
        }
    }
}