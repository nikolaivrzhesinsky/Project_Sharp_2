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
                fileLogic.ShowFile();
                frequency.GetSentimantAnalys(fileLogic.GetText(), i);
                frequency.GetFrequency(fileLogic.GetText(), i);
                
            }

            Singularity svd = new Singularity();
            svd.Decomposition(frequency.MakeFreqMatrix(), out double[,] U, out double[,] S, out double[,] Vt, 2);

            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < U.GetLength(0); i++)
            //{
            //    string line = "";
            //    for (int j = 0; j < U.GetLength(1); j++)
            //    {
            //        line += $"{U[i, j]} ";
            //    }
            //    sb.AppendLine(line);
            //}
            //Console.WriteLine(sb.ToString());
            //sb.Clear();
            //for (int i = 0; i < Vt.GetLength(0); i++)
            //{
            //    string line = "";
            //    for (int j = 0; j < Vt.GetLength(1); j++)
            //    {
            //        line += $"{Vt[i, j]} ";
            //    }
            //    sb.AppendLine(line);
            //}
            //Console.WriteLine(sb.ToString());

            frequency.ShowFreq();

            //var resTuple = DataLayer_BuisnessLayer.Connections.CreateResult(U, S, Vt, frequency.Word_List());

            //DataLayer_BuisnessLayer.Connections.Print(resTuple);
            //frequency.ShowSentAnalys();
            Console.ReadKey();
        }
    }
}
