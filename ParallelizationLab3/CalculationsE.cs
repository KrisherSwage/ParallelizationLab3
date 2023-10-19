using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelizationLab3
{
    internal class CalculationsE : DataForFormulas
    {
        public static void CalcManyE(int leftBor, int rightBor)
        {
            int taskNumber = Convert.ToInt32(Convert.ToInt32(Task.CurrentId) - 1) % fluxes; //-1 решает проблему с неправильным порядком
            for (int i = leftBor; i < rightBor; i++) //цикл для всех E (при N = 24 ~ 16 млн)
            {
                listForDataTask[taskNumber].Add(CalculateE(leftBor, rightBor, i));
            }
        }

        /// <summary>
        /// Метод вычисления определенного количества E. Определенная часть на один поток
        /// </summary>
        /// <param name="leftBor">Левая граница для количества вычисляемых E</param>
        /// <param name="rightBor">Правая граница для количества вычисляемых E - она не включена в вычисления</param>
        private static double CalculateE(int leftBor, int rightBor, int num)
        {
            double result = 0.0;

            var listBigA = ValuesA(num);

            result += AdditivityA(listBigA);

            result += RelationshipA(listBigA);

            return result;
        }

        /// <summary>
        /// Метод перевода числа в значения акторов
        /// </summary>
        /// <param name="num">Число для перевода</param>
        /// <returns>Список новых значений акторов</returns>
        private static List<int> ValuesA(int num)
        {
            string binCodeNum = Convert.ToString(num, 2);
            List<int> valuesA = new List<int>();
            if (binCodeNum.Length < N)
            {
                binCodeNum = string.Concat(Enumerable.Repeat("0", N - binCodeNum.Length)) + binCodeNum;
            }

            if (binCodeNum.Length == N)
            {
                for (int i = 0; i < N; i++) //нолики заменим на -1
                {
                    if (binCodeNum[i] == '0')
                        valuesA.Add(-1);
                    else
                        valuesA.Add(Convert.ToInt32($"{binCodeNum[i]}"));
                }
            }
            else
                return null;

            return valuesA;
        }

        /// <summary>
        /// Часть формулы с одиночными A
        /// </summary>
        /// <param name="bigA"></param>
        /// <returns></returns>
        private static double AdditivityA(List<int> bigA)
        {
            double intermediateResult = 0.0;
            for (int i = 0; i < N; i++)
            {
                intermediateResult += сoefA[i] * bigA[i];
            }
            return intermediateResult;
        }

        /// <summary>
        /// Часть формулы с взаимодействием A
        /// </summary>
        /// <param name="bigA"></param>
        /// <returns></returns>
        private static double RelationshipA(List<int> bigA)
        {
            double intermediateResult = 0.0;
            int itrForB = 0;
            for (int i = 0; i < N - 1; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    intermediateResult += coefB[itrForB] * bigA[i] * bigA[j];
                    itrForB++;
                }
            }
            return intermediateResult;
        }
    }
}
