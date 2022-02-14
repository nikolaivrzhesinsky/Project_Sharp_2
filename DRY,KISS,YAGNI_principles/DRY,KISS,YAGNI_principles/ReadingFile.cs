using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRY_KISS_YAGNI_principles
{
    public class ReadingFile
    {
        private int[,] FormMatrix(string path)
        {
            int rows = 0;
            int columns = 0; 
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                   rows++;
                sr.Close();
            }
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                line = sr.ReadLine();
                string[] subs = line.Split(' ');
                foreach (var sab in subs)
                    columns++;
                sr.Close();
            }

            int[,] matrix = new int[rows, columns];
            return matrix;
        }

        public int[,] Fill_matrix(string path)
        {
            int[,] matrix = FormMatrix(path);

            using (StreamReader sr = new StreamReader(path))
            {
                int index_of_row = 0;
                string line;
                while(!sr.EndOfStream)
                {
                    int index_of_column = 0;
                    line = sr.ReadLine();
                    string[] subs = line.Split(' ');
                    foreach (var sub in subs)
                    {
                        matrix[index_of_row, index_of_column] = Convert.ToInt32(sub);
                        index_of_column++;
                    }
                    index_of_row++;
                }
                sr.Close();
            }
            return matrix;
        }
    }
}
