using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer_BuisnessLayer
{
    public class Matrix_compression
    {
        public int row, col;
        public double[,] matrix = new double[0, 0];
        public List<string> colName;
        public Matrix_compression(int Row, int Col)
        {
            //excep на Row, Col <0
            matrix = new double[Row, Col];
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
        public Matrix_compression(double[,] Matr)
        {
            matrix = Matr;
            row = Matr.GetLength(0);
            col = Matr.GetLength(1);
            colName = new List<string>();
        }
        public Matrix_compression(double[,] Matr, List<string> ColName)
        {
            matrix = Matr;
            row = Matr.GetLength(0);
            col = Matr.GetLength(1);
            colName = ColName;
        }
        public Matrix_compression(int Row, int Col, List<string> ColName)
        {
            //excep на Row, Col <0
            matrix = new double[Row, Col];
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
            //excep на list.count != Row
        }
        public double this[int Row, int Col]
        {
            get
            {
                return this.matrix[Row, Col];
            }

            set
            {
                this.matrix[Row, Col] = value;
            }
        }
        public static Matrix_compression Transpo(Matrix_compression baseMatrix)
        {
            var transpoMatrix = new Matrix_compression(baseMatrix.col, baseMatrix.row);
            for (int i = 0; i < transpoMatrix.col; i++)
            {
                for (int j = 0; j < transpoMatrix.row; j++)
                {
                    transpoMatrix.matrix[j, i] = baseMatrix.matrix[i, j];
                }
            }
            return transpoMatrix;
        }
        public static Matrix_compression MultiMatrix(Matrix_compression firstMatrix, Matrix_compression secondMatrix)
        {
            //excep на firstMatrix.col != secondMatrix.row
            var resMatrix = new Matrix_compression(firstMatrix.row, secondMatrix.col);
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
        public static void MultiNumber(Matrix_compression baseMatrix, int num)
        {
            for (int i = 0; i < baseMatrix.row; i++)
            {
                for (int j = 0; j < baseMatrix.col; j++)
                {
                    baseMatrix.matrix[i, j] *= num;
                }
            }
        }
        public static Matrix_compression SumMatrix(Matrix_compression firstMatrix, Matrix_compression secondMatrix)
        {
            //excep на firstMatrix.row != secondMatrix.row or (col != col)
            var resMatrix = new Matrix_compression(firstMatrix.row, secondMatrix.col);
            for (int i = 0; i < firstMatrix.row; i++)
            {
                for (int j = 0; j < firstMatrix.col; j++)
                {
                    resMatrix[i, j] = firstMatrix[i, j] + secondMatrix[i, j];
                }
            }
            return resMatrix;
        }
    }
    public class Connections
    {
        public static void Print(Matrix_compression Base)
        {
            for (int i = 0; i < Base.row; i++)
            {
                for (int k = 0; k < Base.col; k++)
                {
                    Console.Write(Base[i, k] + " ");
                }
                Console.WriteLine();
            }
        }
        public static List<Tuple<int, string>> Connects(Matrix_compression wordMatrix, Matrix_compression normalizedMatrix, Matrix_compression txtMatrix)
        {

            var resMatrix = new Matrix_compression(wordMatrix.row, txtMatrix.col, wordMatrix.colName);
            for (int i = 0; i < wordMatrix.row; i++)
            {
                for (int k = 0; k < txtMatrix.col; k++)
                {
                    resMatrix[i, k] = 0;
                }
            }
            //Console.WriteLine("txtmat");
            //Print(wordMatrix);
            //Console.WriteLine("wordmat");
            //Print(txtMatrix);
            resMatrix = MultiForDist(wordMatrix, txtMatrix);
            //resMatrix = Matrix.MultiMatrix(wordMatrix, normalizedMatrix);
            //resMatrix = Matrix.MultiMatrix(resMatrix, txtMatrix);
            for (int i = 0; i < resMatrix.row; i++)
            {
                for (int k = 0; k < resMatrix.col; k++)
                {
                    resMatrix[i, k] = Math.Round(resMatrix[i, k], 6);
                }
            }
            //Console.WriteLine("res mat");
            //Print(resMatrix);
            var resTuple = new List<Tuple<int, string>>();
            Tuple<int, string> tmp;
            //for (int i = 0; i < resMatrix.col; i++)
            //{
            //    int id = 0;
            //    for (int k = 1; k < resMatrix.row; k++)
            //    {
            //        if (resMatrix[id, i] < resMatrix[k, i])
            //        {
            //            id = k;
            //        }
            //    }
            //    tmp = new Tuple<int, string>(i + 1, wordMatrix.colName[id]);
            //    resTuple.Add(tmp);
            //}
            for (int i = 0; i < resMatrix.col; i++)
            {
                int id = 0;
                for (int k = 1; k < resMatrix.row; k++)
                {
                    if (resMatrix[id, i] > resMatrix[k, i])
                    {
                        id = k;
                    }
                }
                tmp = new Tuple<int, string>(i + 1, wordMatrix.colName[id]);
                resTuple.Add(tmp);
            }
            return resTuple;
        }

        public static Matrix_compression MultiForDist(Matrix_compression first, Matrix_compression second)
        {
            var res = new Matrix_compression(first.row, second.col);
            for (int i = 0; i < res.row; i++)
            {
                for (int k = 0; k < res.col; k++)
                {
                    for (int j = 0; j < first.col; j++)
                    {
                        res[i, k] += (first[i, j] - second[j, k]) * (first[i, j] - second[j, k]);
                    }
                }
            }

            return res;
        }
        public static List<Tuple<int, string>> CreateResult(double[,] U, double[,] S, double[,] Vt, List<string> word_List)
        {
            var maceMatrix = new Matrix_compression(U, word_List);
            var normMatrix = new Matrix_compression(S);
            var txtMatrix = new Matrix_compression(Vt);



            return Connections.Connects(maceMatrix, normMatrix, txtMatrix); ;
        }
        public static void Print(List<Tuple<int, string>> Base)
        {
            foreach(Tuple<int, string> i in Base)
            {
                Console.WriteLine(i.Item1 + " " + i.Item2);
            }
        }
    }
}
