using Spreadsheet;
//using System;
namespace ConsoleApp6
{
    internal class Connections
    {
        //public static void Print(Matrix Base)
        //{
        //    for(int i=0; i<Base.row; i++)
        //    {
        //        for(int k=0; k<Base.col; k++)
        //        {
        //            Console.Write(Base[i, k] + " ");
        //        }
        //        Console.WriteLine();
        //    }
        //}
        public static List<Tuple<int, string>> Connects(Matrix wordMatrix, Matrix normalizedMatrix, Matrix txtMatrix)
        {

            var resMatrix = new Matrix(wordMatrix.row, txtMatrix.col, wordMatrix.colName);
            for(int i=0; i<wordMatrix.row;i++)
            {
                for (int k=0; k<txtMatrix.col; k++)
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
            for (int i = 0; i<resMatrix.row; i++)
            {
                for (int k = 0; k<resMatrix.col; k++)
                {
                    resMatrix[i, k] = Math.Round(resMatrix[i, k], 6);
                }
            }
            //Console.WriteLine("res mat");
            //Print(resMatrix);
            var resTuple = new List<Tuple<int, string>>();
            Tuple<int, string> tmp;
            //for (int i = 0; i<resMatrix.col; i++)
            //{
            //    int id = 0;
            //    for (int k = 1; k<resMatrix.row; k++)
            //    {
            //        if (resMatrix[id, i]<resMatrix[k, i])
            //        {
            //            id = k;
            //        }
            //    }
            //    tmp = new Tuple<int, string>(i+1, wordMatrix.colName[id]);
            //    resTuple.Add(tmp);
            //}
            for (int i = 0; i<resMatrix.col; i++)
            {
                int id = 0;
                for (int k = 1; k<resMatrix.row; k++)
                {
                    if (resMatrix[id, i]>resMatrix[k, i])
                    {
                        id = k;
                    }
                }
                tmp = new Tuple<int, string>(i+1, wordMatrix.colName[id]);
                resTuple.Add(tmp);
            }
            static Matrix MultiForDist(Matrix first, Matrix second)
            {
                var res = new Matrix(first.row, second.col);
                for (int i = 0; i<res.row; i++)
                {
                    for (int k = 0; k<res.col; k++)
                    {
                        for (int j = 0; j<first.col; j++)
                        {
                            res[i, k] += (first[i, j]-second[j, k]) * (first[i, j]-second[j, k]);
                        }
                    }
                }

                return res;
            }
            return resTuple;
        }
            
    }
}