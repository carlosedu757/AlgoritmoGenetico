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

    }
}
