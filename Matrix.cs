using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet
{
    internal class Matrix
    {
        public int row, col;
        public int[,] matrix = new int[0, 0];
        List<string> colName;
        public Matrix(int Row, int Col)
        {
            matrix = new int[Row, Col];
            row = Row;
            col = Col;
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    matrix[i, j] = 0;
                }
            }
            colName = new List<string>();
        }
        public Matrix(int Row, int Col, List<string> ColName)
        {
            matrix = new int[Row, Col];
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    matrix[i, j] = 0;
                }
            }
            row = Row;
            col = Col;
            colName = ColName;
        }
        public int this[int Row, int Col]
        {
            get
            {
                return this.matrix[Row,Col];
            }

            set
            {
                this.matrix[Row,Col] = value;
            }
        }
        public static Matrix Transpo(Matrix baseMatrix)
        {
            var transpoMatrix = new Matrix(baseMatrix.col, baseMatrix.row);
            for (int i = 0; i < transpoMatrix.col; i++)
            {
                for (int j = 0; j < transpoMatrix.row; j++)
                {
                    transpoMatrix.matrix[j, i] = baseMatrix.matrix[i, j];
                }
            }
            return transpoMatrix;
        }
        public static Matrix MultiMatrix(Matrix firstMatrix, Matrix secondMatrix)
        {
            var resMatrix = new Matrix(firstMatrix.row, secondMatrix.col);
            for (int i = 0; i < firstMatrix.row; i++)
            {
                for (int j = 0; j < secondMatrix.col; j++)
                {
                    for (int k = 0; k < firstMatrix.col; k++)
                    {
                        resMatrix[i, j] += firstMatrix.matrix[i, k] * secondMatrix.matrix[k, j];
                    }
                }
            }
            return resMatrix;
        }
        public static void MultiNumber(Matrix baseMatrix, int num)
        {
            var resMatrix = new Matrix(baseMatrix.col, baseMatrix.row);
            for (int i = 0; i < baseMatrix.row; i++)
            {
                for (int j = 0; j < baseMatrix.col; j++)
                {
                    baseMatrix.matrix[i, j] *= num;
                }
            }
        }
        public static Matrix SumMatrix(Matrix firstMatrix, Matrix secondMatrix)
        {
            var resMatrix = new Matrix(firstMatrix.row, secondMatrix.col);
            for (int i = 0; i < firstMatrix.row; i++)
            {
                for (int j = 0; j < firstMatrix.col; j++)
                {
                    resMatrix[i, j] = firstMatrix[i, j]+secondMatrix[i, j];
                }
            }
            return resMatrix;
        }
    }
}
