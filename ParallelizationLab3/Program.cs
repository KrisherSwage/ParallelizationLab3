using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelizationLab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StartE();

        }

        private static void StartE()
        {
            int n = 22;

            //TestForAllFluxes(n); //для замера времени

            Experiment(n);
            DataForFormulas.CollectionAllE(); // Е из потоков в один общий список ----- ALERT!!!
            var E = DataForFormulas.valuesE;
            Console.WriteLine($"E.Max() = {E.Max()}");

            CalculationsAverAmax.TableRo(); //таблица ро с тильдой

            CalculationsAverAmax.AverageAmax();
            WritingFile.FileForGraphAverAmax(DataForFormulas.avAmax, DataForFormulas.valuesT, "Amax"); //значения <Amax> записываем в файл

            E.Sort(); //16 млн за ~2 сек // ---------- сортировку не проводить до того, как все рассчитается!!!
            WritingFile.FileForGraphEndBeg(E, "GraphE"); //значения E записываем в файл
        }

        private static void Experiment(int n)
        {
            int fluxes = 12;
            DataForFormulas.InitialConditionsData(n, fluxes); //инициализация и объявление исходных данных

            Stopwatch time = new Stopwatch(); //время
            time.Start(); //время

            ParallelizationCode.StartParalCalc(); // --- запуск ||

            time.Stop(); //время

            double myTime = time.ElapsedMilliseconds / 1000.0;
            Console.WriteLine($"потоков = {fluxes}\tвремя = {myTime} сек"); //время
        }

        private static void TestForAllFluxes(int n)
        {
            double[,] timeTable = new double[11, 12];
            Console.WriteLine(n);
            for (int i = 0; i < 12; i++)
            {
                int fluxes = i + 1;
                timeTable[0, i] = fluxes;
                for (int j = 0; j < 10; j++)
                {
                    DataForFormulas.InitialConditionsData(n, fluxes); //инициализация и объявление исходных данных

                    Stopwatch time = new Stopwatch(); //время
                    time.Start(); //время

                    ParallelizationCode.StartParalCalc(); // --- запуск ||

                    time.Stop(); //время

                    double myTime = time.ElapsedMilliseconds / 1000.0;
                    Console.WriteLine($"потоков = {fluxes}\tвремя = {myTime} сек"); //время

                    DataForFormulas.CollectionAllE(); // Е из потоков в один общий список ----- ALERT!!!
                    var E = DataForFormulas.valuesE;
                    Console.WriteLine($"E.Max() = {E.Max()}");

                    timeTable[j + 1, i] = myTime;
                }
                Console.WriteLine();

            }
            WritingFile.FileForGraphAverAmax(timeTable, "Time");
        }
    }
}