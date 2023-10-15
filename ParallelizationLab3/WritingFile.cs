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
        public static void CreateResultCSV(double[,] myData)
        {
            string pathWriteData = Path.Combine(Environment.CurrentDirectory, "ParallionLab3.csv");

            using (StreamWriter sw = new StreamWriter(pathWriteData, false, Encoding.UTF8))
            {
                for (int i = 0; i < myData.GetLength(0); i++)
                {
                    for (int j = 0; j < myData.GetLength(1); j++)
                    {
                        sw.Write($"{myData[i, j]};");
                    }
                    sw.WriteLine();
                }
            }
        }

        public static void FileForGraphEndBeg(List<double> myList, string fileName)
        {
            string pathWriteData = Path.Combine(Environment.CurrentDirectory, $"{fileName}.csv");

            using (StreamWriter sw = new StreamWriter(pathWriteData, false, Encoding.UTF8))
            {
                for (int i = myList.Count - 1; i >= 0; i -= 1)
                {
                    sw.WriteLine($"{myList[i]};");
                }
                sw.WriteLine($"{myList[0]};");

            }
        }

        public static void FileForGraphBegEnd(List<double> myList, string fileName)
        {
            string pathWriteData = Path.Combine(Environment.CurrentDirectory, $"{fileName}.csv");

            using (StreamWriter sw = new StreamWriter(pathWriteData, false, Encoding.UTF8))
            {
                for (int i = 0; i < myList.Count - 1; i++)
                {
                    sw.WriteLine($"{myList[i]};");
                }
                sw.WriteLine($"{myList[0]};");

            }
        }
    }
}
