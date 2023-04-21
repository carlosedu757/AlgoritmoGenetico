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
                List<(decimal, string)> probabilidades = new List<(decimal, string)>();
                Console.WriteLine("-----------------------------------------------------");
                if (i == 0)
                    Console.WriteLine("Populacao Inicial");
                else
                    Console.WriteLine(i + "° GERACAO");

                Console.WriteLine("Cromossomo | x | f(x) | Prob. Seleção");

                for (int k = 1; k <= 4; k++)
                {

                    bool negativo = population[k - 1] < 0;
                    int valorAbsoluto = Math.Abs(population[k - 1]);

                    string numeroBinario = Convert.ToString(valorAbsoluto, 2).PadLeft(4, '0');

                    int fx = ProgramTRA.FuncaoQuadratica(population[k - 1]);
                    int fxSoma = population.Select(x => ProgramTRA.FuncaoQuadratica(x)).Sum();
                    decimal probabilidade = Math.Round(ProgramTRA.FuncaoProbabilidade(fx, (decimal)fxSoma) * 100, 2);

                    if (negativo)
                        numeroBinario = "1" + numeroBinario;
                    else
                        numeroBinario = "0" + numeroBinario;

                    probabilidades.Add((probabilidade, numeroBinario));

                    Console.WriteLine(k + " | " + numeroBinario + " | " + population[k - 1] + " | " + fx + " | " + probabilidade + "%");
                }

                Console.WriteLine("\nCRIAÇÃO DE UMA NOVA GERAÇÃO...\n");

                List<(decimal, string)> maioresProbabilidades = probabilidades.OrderByDescending(tupla => tupla.Item1).Take(2).ToList();

                Console.WriteLine("CROSSOVER\n");

                List<string> crossovers = new List<string>();

                crossovers.Add(maioresProbabilidades[0].Item2[..2] + maioresProbabilidades[1].Item2[2..5]);
                crossovers.Add(maioresProbabilidades[1].Item2[..2] + maioresProbabilidades[0].Item2[2..5]);

                crossovers.Add(maioresProbabilidades[1].Item2[..3] + maioresProbabilidades[0].Item2[3..5]);
                crossovers.Add(maioresProbabilidades[0].Item2[..3] + maioresProbabilidades[1].Item2[3..5]);

                Console.WriteLine("1 - " + maioresProbabilidades[0].Item2[..2] + "|" + maioresProbabilidades[0].Item2[2..5] + " ---> " + crossovers[0]);
                Console.WriteLine("2 - " + maioresProbabilidades[1].Item2[..2] + "|" + maioresProbabilidades[1].Item2[2..5] + " ---> " + crossovers[1]);
                Console.WriteLine("3 - " + maioresProbabilidades[1].Item2[..3] + "|" + maioresProbabilidades[1].Item2[3..5] + " ---> " + crossovers[2]);
                Console.WriteLine("4 - " + maioresProbabilidades[0].Item2[..3] + "|" + maioresProbabilidades[0].Item2[3..5] + " ---> " + crossovers[3]);

                Console.WriteLine("\nMUTACAO\n");

                List<string> mutacoes = new List<string>();

                foreach (string crossover in crossovers)
                {
                    StringBuilder sb = new StringBuilder(crossover);

                    for(int j = 0; j < 5; j++)
                    {
                        int random = new Random().Next(1, 100);

                        if (random == 1)
                        {
                            sb.Remove(j, 1); // remove o primeiro caractere
                            if (crossover[j] == '0')
                                sb.Insert(j, '1'); // insere 'E' no início da string
                            else
                                sb.Insert(j, '0');
                        }
                    }
                    mutacoes.Add(sb.ToString());
                }

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
            }while(i <= 20);
        }
    }
}