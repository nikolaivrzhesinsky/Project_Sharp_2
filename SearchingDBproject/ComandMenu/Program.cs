using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchingLibrary;
using System.IO;

namespace ComandMenu
{
    public class Program
    {
       

        public static void Main(string[] args)
        {
            DataBase dataBase = new DataBase();
            FileLogic fileLogic = new FileLogic();
            Frequency frequency = new Frequency();

            string[] array = Directory.GetFiles(@"C:\Users\абв\Documents\GitHub\Project_Sharp_2\SearchingDBproject\Тексты для АНАЛиза");

           

            for (int i = 0; i < array.Length; i++)
            {
                fileLogic.GetFile(array[i]);
                fileLogic.ShowFile();
                frequency.GetFrequency(fileLogic.GetText(), i);

            }
            frequency.MakeFreqMatrix();
            frequency.ShowFreqMatrix();
           // frequency.ShowFreq();
           
            


            Console.ReadLine();
            
            
            
            
        }
    }
}
