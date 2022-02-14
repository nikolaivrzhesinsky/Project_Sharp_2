using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DRY_KISS_YAGNI_principles;


namespace Testing_principles
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"origin_matrix.txt";
            Matrix_operations mo = new Matrix_operations(path);
            int[,] matrix = mo.Matrix_transpose();
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < matrix.GetLength(0); i ++)
            {
                string line = "";
                for(int j = 0; j < matrix.GetLength(1); j++)
                {
                    line += matrix[i, j].ToString() + " ";
                    
                }
                sb.AppendLine(line);
            }
            Console.WriteLine(sb.ToString());
            Console.ReadKey();

        }
    }
}
