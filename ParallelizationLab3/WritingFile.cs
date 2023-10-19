using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ParallelizationLab3
{
    internal class WritingFile
    {
        public static void FileForGraphEndBeg(List<double> myList, string fileName) //не все E, по убыванию
        {
            string pathWriteData = Path.Combine(Environment.CurrentDirectory, $"{fileName}.csv");

            using (StreamWriter sw = new StreamWriter(pathWriteData, false, Encoding.UTF8))
            {
                for (int i = myList.Count - 1; i >= 0; i -= 100)
                {
                    sw.WriteLine($"{myList[i]};");
                }
                sw.WriteLine($"{myList[0]};");

            }
        }

        public static void FileForGraphAverAmax(List<double> myListA, List<double> myListT, string fileName) //<Amax> и T
        {
            string pathWriteData = Path.Combine(Environment.CurrentDirectory, $"{fileName}.csv");

            using (StreamWriter sw = new StreamWriter(pathWriteData, false, Encoding.UTF8))
            {
                sw.WriteLine($"<Amax>;T;");
                for (int i = 0; i < myListA.Count - 1; i++)
                {
                    sw.WriteLine($"{myListA[i]};{myListT[i]};");
                }

            }
        }

        public static void FileForGraphAverAmax(double[,] myArr, string fileName) //для времени по разному количеству потоков
        {
            string pathWriteData = Path.Combine(Environment.CurrentDirectory, $"{fileName}.csv");

            using (StreamWriter sw = new StreamWriter(pathWriteData, false, Encoding.UTF8))
            {
                for (int i = 0; i < myArr.GetLength(0); i++)
                {
                    for (int j = 0; j < myArr.GetLength(1); j++)
                    {
                        sw.Write($"{myArr[i, j]};");
                    }
                    sw.WriteLine();
                }
            }

        }

    }
}
