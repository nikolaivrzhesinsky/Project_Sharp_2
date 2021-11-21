using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ExceptionsLibrary
{
   public class TriangleException: GeometryException {

        public TriangleException() : base() { }

        private string TandQpath = @"C:\Users\HYPERPC\Desktop\TandQEx.txt";

        public override string Message
        {
            get
            {
                return "Невозможно создать треугольник для указанных длин сторон";
            }
        }
        public override void FileWriter()
        {
            
            File.AppendAllText(path, exception + Environment.NewLine);
            File.AppendAllText(TandQpath, Message + Environment.NewLine);
            
        }
    }

    
}
