using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRY_KISS_YAGNI_principles
{
    public class Matrix_operations
    {
        private string path { get; set; }
        private int[,] matrix { get; set; }
        private ReadingFile rf = new ReadingFile();
        public Matrix_operations(string _path)
        {
            path = _path;
            matrix = rf.Fill_matrix(path);

        }
        public int[,] Matrix_transpose()
        {
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);

            int[,] transposed_matrix = new int[n, m];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    transposed_matrix[j, i] = matrix[i, j];
                }
            }

            return transposed_matrix;
        }
    }
}
