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

            string[] array = Directory.GetFiles(@"C:\Users\HYPERPC\Desktop\texts");

            for (int i = 0; i < array.Length; i++)
            {
                fileLogic.GetFile(array[i]);
                
                frequency.GetFrequency(fileLogic.GetText(), i);

            }
            
            //fileLogic.ShowFile();
            //frequency.ShowFreq(fileLogic.GetText());

            Console.ReadLine();
            

            
            
        }
    }
}
