using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ExceptionsLibrary
{
   public class GeometryException: Exception
    {
        public GeometryException(): base() { }

        public string path = @"C:\Users\HYPERPC\Desktop\GeometryException.txt";

        public string exception = "Невозможно создать фигуру";

        
        private int[] array = new int[4];
        public int[] Parametrs
        {
            get { return array; }
            private set
            {
                array = value;
            }
        }
        public virtual void FileWriter()
        {
            
            File.AppendAllText(path, exception + Environment.NewLine);
        }


    }
}
