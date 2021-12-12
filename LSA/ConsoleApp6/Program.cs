using ConsoleApp6;
using Spreadsheet;
using System;
using System.Collections.Generic;
using Matrix_operations;
using SearchingLibrary;

namespace Exmaple
{
    class Program
    {
        static void Main()
        {
            DataBase dataBase = new DataBase();
            FileLogic fileLogic = new FileLogic();
            FrequencyAndSentimentAnalys frequency = new FrequencyAndSentimentAnalys();

            string[] array = Directory.GetFiles(@"texts_for_analysis");

            for (int i = 0; i < array.Length; i++)
            {
                fileLogic.GetFile(array[i]);
                fileLogic.ShowFile();
                frequency.GetFrequency(fileLogic.GetText(), i);

            }

            var svd = new Matrix_operations.Singularity();
            svd.Decomposition(frequency.MakeFreqMatrix(), out double[,] U, out double[,] S, out double[,] Vt);
            var maceMatrix = new Matrix(U, frequency.Word_List());
            var normMatrix = new Matrix(S);
            var txtMatrix = new Matrix(Vt);
            var resTuple = Connections.Connects(maceMatrix, normMatrix, txtMatrix);
            foreach (Tuple<int, string> item in resTuple)
            {
                Console.WriteLine(item.Item1 + " " + item.Item2);
            };
        }
    }
}