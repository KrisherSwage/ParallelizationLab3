using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelizationLab3
{
    internal class CalculationsAverAmax : DataForFormulas
    {
        public static void TableRo()
        {
            for (int i = 0; i < valuesT.Count; i++)
            {
                double sum = 0.0;
                for (int j = 0; j < valuesE.Count; j++)
                {
                    double ro = Math.Exp(valuesE[j] / valuesT[i]);
                    sum += ro;
                }

                for (int j = 0; j < valuesE.Count; j++)
                {
                    double roTil = Math.Exp(valuesE[j] / valuesT[i]) / sum;

                    valuesRoTilda[i].Add(roTil);
                }
            }
        }

        public static void AverageAmax()
        {
            for (int i = 0; i < valuesT.Count; i++)
            {
                double res = 0.0;

                for (int j = 0; j < Math.Pow(2, N); j++)
                {
                    res += seqAmax[j] * valuesRoTilda[i][j];
                }
                avAmax.Add(res);
            }

        }
    }
}
