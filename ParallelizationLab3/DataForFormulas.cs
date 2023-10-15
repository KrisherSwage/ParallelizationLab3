using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelizationLab3
{
    internal class DataForFormulas
    {
        /// <summary>
        /// Количество потоков
        /// </summary>
        private protected static int fluxes;
        /// <summary>
        /// Общее число акторов
        /// </summary>
        private protected static int N;

        private static Random rnd = new Random();

        /// <summary>
        /// Случайные коэффициенты для акторов
        /// </summary>
        private protected static List<double> сoefA = new List<double>(); // количество = N
        /// <summary>
        /// Случайные коэффициенты для взаимоотношения акторов
        /// </summary>
        private protected static List<double> coefB = new List<double>(); // количество = ((N - 1) * N) / 2
        /// <summary>
        /// Прибыльность - то, что мы ищем
        /// </summary>
        public static List<double> valuesE = new List<double>();
        // количество равно 2^N (2 - т.к. значения A либо -1, либо 1)
        //16777216 (10) = 1.000.000.000.000.000.000.000.000 (2) //24 нуля и одна единица
        /// <summary>
        /// Максимальное значение прибыльности (E)
        /// </summary>
        public static double maxValueE;
        /// <summary>
        /// Список для результатов рассчитанных E в разных потоках 
        /// </summary>
        private protected static List<List<double>> listForDataTask = new List<List<double>>();

        // ----- для второй части -----

        /// <summary>
        /// Значения T (параметра неопределенности)
        /// </summary>
        public static List<double> valuesT = new List<double>();
        /// <summary>
        /// Таблица значений ро с тильдой
        /// </summary>
        public static List<List<double>> valuesRoTilda = new List<List<double>>();
        /// <summary>
        /// Индекс максимального a и соответствубщего ему Amax
        /// </summary>
        public static int indexMaxA;
        /// <summary>
        /// Значения Amax - массив размерности 2^N, состоящий из -1 и 1 в определенном порядке
        /// </summary>
        public static List<int> seqAmax = new List<int>();
        /// <summary>
        /// Значения <Amax> - метод расчета в классе CalculationsAverAmax
        /// </summary>
        public static List<double> avAmax = new List<double>();

        public static void InitialConditionsData(int n, int fl)
        {
            N = n;
            fluxes = fl;

            ValuesCoefA();
            ValuesCoefB();

            FindIndexMaxA();
            SequenceAmax();

            for (int i = 0; i < fluxes; i++)
            {
                listForDataTask.Add(new List<double>());
            }

            ValuesT();
            numberRo();
        }

        /// <summary>
        /// Коэффициенты a (малое)
        /// </summary>
        private static void ValuesCoefA()
        {
            for (int i = 0; i < N; i++)
            {
                сoefA.Add(СoefficientGeneration());
            }
        }

        /// <summary>
        /// Коэффициенты b
        /// </summary>
        private static void ValuesCoefB()
        {
            for (int i = 0; i < ((N - 1) * N) / 2; i++)
            {
                coefB.Add(СoefficientGeneration());
            }
        }

        /// <summary>
        /// Значения коэффициентов
        /// </summary>
        /// <returns></returns>
        private static double СoefficientGeneration() //для коэффициетов a и b
        {
            double res = rnd.NextDouble() * (1.0 - (-1.0)) + (-1.0);
            return res;
        }

        // ALERT! Скорее всего, первый отработавший поток - не первый по порядку. Могут сбиться соотношения A и E
        /// <summary>
        /// Запись E из потоков в общий список
        /// </summary>
        public static void CollectionAllE() //16 млн ~0,3 сек
        {
            for (int i = 0; i < listForDataTask.Count; i++)
            {
                for (int j = 0; j < listForDataTask[i].Count; j++)
                {
                    valuesE.Add(listForDataTask[i][j]);
                }
            }
        }

        /// <summary>
        /// Некоторые значения T
        /// </summary>
        public static void ValuesT()
        {
            for (double i = 0.1; i < 1000000; i *= 2)
            {
                valuesT.Add(i);
            }
        }

        /// <summary>
        /// Список списков для Ро
        /// </summary>
        public static void numberRo()
        {
            for (int i = 0; i < valuesT.Count; i++)
            {
                valuesRoTilda.Add(new List<double>());
            }
        }

        /// <summary>
        /// Индекс максимального a
        /// </summary>
        public static void FindIndexMaxA()
        {
            double max = сoefA.Max();

            indexMaxA = сoefA.IndexOf(max);
        }

        /// <summary>
        /// Значения A для индекса a максимального
        /// </summary>
        public static void SequenceAmax()
        {
            int multiple = N - indexMaxA - 1;
            int val = -1;
            for (int i = 1; i < Math.Pow(2, N) + 1; i++)
            {
                seqAmax.Add(val);
                if (i % Math.Pow(2, multiple) == 0)
                {
                    val *= -1;
                }
            }
        }


    }
}
