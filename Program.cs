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
                int numAleatorio = new Random().Next(-10, 10);

                if(!population.Exists(x => x == numAleatorio))
                    population.Add(numAleatorio);
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

                    string numeroBinario = Convert.ToString(valorAbsoluto, 2).PadLeft(4, '0');

                    int fx = ProgramTRA.FuncaoQuadratica(population[k - 1]);

                    if (negativo)
                        numeroBinario = "1" + numeroBinario;
                    else
                        numeroBinario = "0" + numeroBinario;

                    aptos.Add((fx, numeroBinario));

                    Console.WriteLine(k + " | " + numeroBinario + " | " + population[k - 1] + " | " + fx);
                }

                Console.WriteLine("\nCRIAÇÃO DE UMA NOVA GERAÇÃO...\n");

                List<(int, string)> maisAptos = ProgramTRA.SelecaoTorneio(aptos); // SELECAO POR TORNEIO

                Console.WriteLine("CROSSOVER\n");

                List<string> crossovers = ProgramTRA.Crossover(maisAptos);

                Console.WriteLine("1 - " + maisAptos[0].Item2[..2] + "|" + maisAptos[0].Item2[2..5] + " ---> " + crossovers[0]);
                Console.WriteLine("2 - " + maisAptos[1].Item2[..2] + "|" + maisAptos[1].Item2[2..5] + " ---> " + crossovers[1]);
                Console.WriteLine("3 - " + maisAptos[1].Item2[..3] + "|" + maisAptos[1].Item2[3..5] + " ---> " + crossovers[2]);
                Console.WriteLine("4 - " + maisAptos[0].Item2[..3] + "|" + maisAptos[0].Item2[3..5] + " ---> " + crossovers[3]);

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
                    int numerox = Convert.ToInt32(mutacao[1..5], 2);
                    if (mutacao[0] == '1')
                        numerox = -numerox;

                    population.Add(numerox);
                }

                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine("\n");
                i++;
            }while(i <= 5);
        }
    }
}