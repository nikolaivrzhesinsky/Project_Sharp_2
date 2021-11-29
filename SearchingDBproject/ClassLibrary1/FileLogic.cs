using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchingLibrary
{
    public class FileLogic
    {
        private String path = @"C:\Users\абв\Desktop\test2.txt";
        private List<string> text = new List<string>();

        public void GetFile()
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                while (!streamReader.EndOfStream)
                {
                    string[] array = streamReader.ReadLine().Split();
                    for (int i = 0; i < array.Length; i++)
                    {
                        text.Add(array[i]);
                    }
                }
            }
        }

        public List<string> GetText()
        {
            return text;
        }

        public void ShowFile()
        {
            foreach(string i in text)
            {
                Console.WriteLine(i);
            }
        }


    }
}
