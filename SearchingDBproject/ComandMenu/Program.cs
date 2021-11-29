using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchingLibrary;

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
        }
    }
}
