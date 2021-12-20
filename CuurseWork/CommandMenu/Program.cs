using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer_BuisnessLayer;
using System.IO;

namespace CommandMenu
{
    class Program
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
                frequency.GetSentimantAnalys(fileLogic.GetText(), i);
                frequency.GetFrequency(fileLogic.GetText(), i);

            }

            Singularity svd = new Singularity();
            svd.Decomposition(frequency.MakeFreqMatrix(), out double[,] U, out double[,] S, out double[,] Vt, 2);

            var maceMatrix = new Matrix_compression(U, frequency.Word_List());
            Connections.Print(maceMatrix);
            var normMatrix = new Matrix_compression(S);
            var txtMatrix = new Matrix_compression(Vt);
            var resTuple = Connections.Connects(maceMatrix, normMatrix, txtMatrix);
            foreach (Tuple<int, string> item in resTuple)
            {
                Console.WriteLine(item.Item1 + " " + item.Item2);
            };
            frequency.ShowSentAnalys();
            Console.ReadKey();
        }
    }
}
