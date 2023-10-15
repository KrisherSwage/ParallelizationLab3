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
            int fluxes = 12;
            int n = 22;

            //DataForFormulas dataForFormulas = new DataForFormulas(n); //разобраться в теории наследования конструкторов
            //ParallelizationCode parallelization = new ParallelizationCode(fluxes);

            DataForFormulas.InitialConditionsData(n, fluxes); //инициализация и объявление исходных данных

            Stopwatch time = new Stopwatch(); //время
            time.Start(); //время

            ParallelizationCode.StartParalCalc(); // --- запуск ||

            time.Stop(); //время

            DataForFormulas.CollectionAllE(); // Е из потоков в один общий список ----- ALERT!!!

            double timeRes = time.ElapsedMilliseconds / 1000.0; //время
            Console.WriteLine($"время = {timeRes}"); //время

            var E = DataForFormulas.valuesE;
            //e.Sort(); //16 млн за ~2 сек // ---------- сортировку не проводить до того, как все рассчитается!!!

            Console.WriteLine($"e.Max() = {E.Max()}");
            Console.WriteLine(Math.Pow(2, n) == E.Count);
            Console.WriteLine(Math.Pow(2, n) - E.Count);

            WritingFile.FileForGraphEndBeg(E, "GraphE"); //значения E записываем в файл

            Stopwatch time1 = new Stopwatch(); //время
            time1.Start(); //время

            CalculationsAverAmax.TableRo(); //таблица ро с тильдой ------- ТРЕБУЕТ ||
            //Calculations.VectorRo();

            time1.Stop(); //время

            Console.WriteLine($"время на таблицу = {time1.ElapsedMilliseconds / 1000.0}"); //время

            //Calculations.AverageAmax();
            CalculationsAverAmax.AverageAmax();
            WritingFile.FileForGraphBegEnd(DataForFormulas.avAmax, "Amax"); //значения <Amax> записываем в файл
        }
    }
}