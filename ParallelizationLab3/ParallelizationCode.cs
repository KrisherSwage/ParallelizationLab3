using System;
using System.Threading.Tasks;

namespace ParallelizationLab3
{
    internal class ParallelizationCode : DataForFormulas
    {
        /// <summary>
        /// Общее количество E
        /// </summary>
        private static int amountOfE;

        private static int amntEInFlux;

        private static void InitialConditionsParall()
        {
            amountOfE = Convert.ToInt32(Math.Pow(2, N));
            amntEInFlux = amountOfE / fluxes; //потерь не будет - исправил
        }

        public static void StartParalCalc()
        {
            InitialConditionsParall();
            Task[] tasks = new Task[fluxes]; //массив для созданных потоков

            for (int i = 0; i < fluxes; i++)
            {
                var leftBor = amntEInFlux * i;
                var rightBor = amntEInFlux * (i + 1);

                if (i == fluxes - 1) //нет потерь
                {
                    rightBor = amountOfE;
                }

                //Console.WriteLine($"{leftBor} - {rightBor}");
                tasks[i] = Task.Run(() => CalculationsE.CalcManyE(leftBor, rightBor)); //создаем и запускаем новый поток с функцией расчета
            }

            Task.WaitAll(tasks);
        }
    }
}
