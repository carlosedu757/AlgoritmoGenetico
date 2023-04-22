using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoGenetico.functions
{
    public class ProgramTRA
    {
        public static int FuncaoQuadratica(int n)
        {
            return ((int)(Math.Pow(n, 2) - (3 * n) + 4));
        }

        public static decimal FuncaoProbabilidade(int n, decimal divisor)
        {
            return (1 - (n / divisor));
        }

        public static List<(int, string)> SelecaoTorneio(List<(int, string)> aptos)
        {
            List<(int, string)> maisAptos = new List<(int, string)>();

            while (maisAptos.Count < 2)
            {
                int index1 = new Random().Next(0, 4);
                int index2 = new Random().Next(0, 4);

                while (index1 == index2) // verificando se os números gerados são iguais
                {
                    index2 = new Random().Next(0, 4); // gerando um novo número aleatório para index2
                }

                (int, string) aptosAleatorio1 = aptos[index1];
                (int, string) aptosAleatorio2 = aptos[index2];

                if (aptosAleatorio1.Item1 <= aptosAleatorio2.Item1)
                    maisAptos.Add(aptosAleatorio1);
                else
                    maisAptos.Add(aptosAleatorio2);
            }

            return maisAptos;
        }

        public static List<string> Crossover(List<(int, string)> maisAptos)
        {
            List<string> filhos = new List<string>();
            while(filhos.Count < 4)
            {
                int moeda = new Random().Next(1, 6);
                if (moeda == 4 || moeda == 5)
                {
                    filhos.Add(maisAptos[0].Item2);
                    filhos.Add(maisAptos[1].Item2);
                    Console.WriteLine(maisAptos[0].Item2 + " ---> " + maisAptos[0].Item2);
                    Console.WriteLine(maisAptos[1].Item2 + " ---> " + maisAptos[1].Item2);
                }
                else
                {
                    int corte = new Random().Next(2, 4);
                    filhos.Add(maisAptos[0].Item2[..corte] + maisAptos[1].Item2[corte..5]);
                    filhos.Add(maisAptos[1].Item2[..corte] + maisAptos[0].Item2[corte..5]);
                    Console.WriteLine(maisAptos[0].Item2[..corte] + "|" + maisAptos[0].Item2[corte..5] + " ---> " + maisAptos[0].Item2[..corte] + maisAptos[1].Item2[corte..5]);
                    Console.WriteLine(maisAptos[1].Item2[..corte] + "|" + maisAptos[1].Item2[corte..5] + " ---> " + maisAptos[1].Item2[..corte] + maisAptos[0].Item2[corte..5]);
                }
            }
            return filhos;
        }

        public static List<string> Mutacoes(List<string> crossovers)
        {
            List<string> mutacoes = new List<string>();

            foreach (string crossover in crossovers)
            {
                StringBuilder sb = new StringBuilder(crossover);

                for (int j = 0; j < 5; j++)
                {
                    int random = new Random().Next(0, 100);

                    if (random == 1)
                    {
                        sb.Remove(j, 1);
                        if (crossover[j] == '0')
                            sb.Insert(j, '1');
                        else
                            sb.Insert(j, '0');
                    }
                }
                mutacoes.Add(sb.ToString());
            }

            return mutacoes;
        }
    }
}
