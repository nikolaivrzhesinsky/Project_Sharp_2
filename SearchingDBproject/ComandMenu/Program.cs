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

            fileLogic.GetFile();
            //fileLogic.ShowFile();
            frequency.ShowFreq(fileLogic.GetText());

            Console.ReadLine();
            

            DirectoryInfo di = new DirectoryInfo(@"C:\Users\HYPERPC\Desktop\texts");
            int i = 0;
            if (di.Exists)
            {
                // ищем в корневом каталоге
                i += di.GetFiles().Length;
            }
            Console.WriteLine(i.ToString());
            Console.ReadLine();
        }
    }
}
