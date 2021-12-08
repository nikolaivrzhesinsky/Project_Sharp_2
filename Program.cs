using Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    public static void Main()
    {
        var test = new Matrix(2, 2);
        for (int i=0; i<test.row;i++)
        {
            for(int j=0; j<test.col; j++)
            {
                test[i, j] = i + j*2;
                Console.Write(test[i, j] + " ");
            }
            Console.WriteLine();
        }
        test = Matrix.MultiMatrix(test, test);
        for (int i = 0; i < test.row; i++)
        {
            for (int j = 0; j < test.col; j++)
            {
                Console.Write(test[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}