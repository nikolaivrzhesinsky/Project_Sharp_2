using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ExceptionsLibrary
{
    public class QuadrangleException: GeometryException
    {
        public QuadrangleException() : base() { }

        private string TandQpath = @"C:\Users\HYPERPC\Desktop\TandQEx.txt";
        public override string Message
        {
            get
            {
                return "Невозможно создать четырехугольник для указанных длин сторон";
            }
        }
        public override void FileWriter()
        {
            
            File.AppendAllText(path, exception + Environment.NewLine);
            File.AppendAllText(TandQpath, Message + Environment.NewLine);
            
        }
    }
}
