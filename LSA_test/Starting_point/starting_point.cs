using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrix_operations;
using FirstStep_Library;
using Spreadsheet;
using System.IO;

namespace Starting_point
{
    class starting_point
    {
        static void Main(string[] args)
        {
            DataBase dataBase = new DataBase();
            FileLogic fileLogic = new FileLogic();
            FrequencyAndSentimentAnalys frequency = new FrequencyAndSentimentAnalys();

            string[] array = Directory.GetFiles(@"texts_for_analysis");

            for (int i = 0; i < array.Length; i++)
            {
                fileLogic.GetFile(array[i]);
                frequency.GetFrequency(fileLogic.GetText(), i);

            }

            Singularity svd = new Singularity();
            svd.Decomposition(frequency.MakeFreqMatrix(), out double[,] U, out double[,] S, out double[,] Vt, 2);
            
            var maceMatrix = new Matrix(U, frequency.Word_List());
            Connections.Print(maceMatrix);
            var normMatrix = new Matrix(S);
            var txtMatrix = new Matrix(Vt);
            var resTuple = Connections.Connects(maceMatrix, normMatrix, txtMatrix);
            foreach (Tuple<int, string> item in resTuple)
            {
                Console.WriteLine(item.Item1 + " " + item.Item2);
            };
            Console.ReadKey();
        }
    }
}
