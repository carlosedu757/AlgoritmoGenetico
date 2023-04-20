using AlgoritmoGenetico.functions;
using System;
using System.ComponentModel.Design;
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
                if(i == 0)
                    Console.WriteLine("Populacao Inicial");
                else
                    Console.WriteLine(i + "° GERACAO");

                Console.WriteLine("Cromossomo | x | f(x) | Prob. Seleção");

                for (int k = 1; k <= 4; k++)
                {
                    string numeroBinario = Convert.ToString(population[k - 1], 2);

                    int fx = ProgramTRA.FuncaoQuadratica(population[k - 1]);
                    int fxSoma = population.Select(x => ProgramTRA.FuncaoQuadratica(x)).Sum();
                    decimal probabilidade = Math.Round(ProgramTRA.FuncaoProbabilidade(fx, (decimal)fxSoma) * 100, 2);

                    Console.WriteLine(k + " - " + numeroBinario + " | " + population[k - 1] + " | " + fx + " | " + probabilidade + "%");

                }
                Console.WriteLine("\n");
                i++;
            }while(i <= 5);
            Console.WriteLine("Hello, World!");
        }
    }
}