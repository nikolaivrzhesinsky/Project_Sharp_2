namespace Spreadsheet
{
    internal class Matrix
    {
        public int row, col;
        public double[,] matrix = new double[0, 0];
        public List<string> colName;
        public Matrix(int Row, int Col)
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
        public Matrix(double[,] Matr)
        {
            matrix = Matr;
            row = Matr.GetLength(0);
            col = Matr.GetLength(1);
            colName = new List<string>();
        }
        public Matrix(double[,] Matr,List<string> ColName)
        {
            matrix = Matr;
            row = Matr.GetLength(0);
            col = Matr.GetLength(1);
            colName = ColName;
        }
        public Matrix(int Row, int Col, List<string> ColName)
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
            //excep на firstMatrix.col != secondMatrix.row
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
            //excep на firstMatrix.row != secondMatrix.row or (col != col)
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
