using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
//using System.Management.Automation;
using System.Threading.Tasks;

namespace Matrix_operations
{
    //public class NumPyInstallation
    //{
    //    public void Task()
    //    {
    //        PowerShell ps = PowerShell.Create();
    //        ps.AddScript(File.ReadAllText(@"PSscript\Psscript.txt")).Invoke();
    //    }
    //}
    public class Singularity
    {
       
        private double[,] Matrix_Transpose(double[,] matrix)
        {

            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);

            double[,] transposed_matrix = new double[n, m];
            for(int i = 0; i < m; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    transposed_matrix[j, i] = matrix[i, j];
                }
            }
            return transposed_matrix;
        }
        public double [,] Matrix_multiplier(double [,] matrix1, double[,] matrix2)
        {
            if (matrix1.GetLength(1) != matrix2.GetLength(0))
                throw new Exception("Матрицы нельзя перемножить");

            double[,] result = new double[matrix1.GetLength(0),matrix2.GetLength(1)];

            for(int i = 0; i < matrix1.GetLength(0); i++)
            {
                for(int j = 0; j < matrix2.GetLength(1); j++)
                {
                    for(int k = 0; k < matrix2.GetLength(0); k++)
                    {
                        result[i,j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }

            return result;
        }

        private double[] Eigenvalues_ascending(double[,] origin_matrix)
        {
            FileStream fs = File.Create(@"origin_matrix.txt");
            fs.Close();
            using (StreamWriter sw = new StreamWriter(@"origin_matrix.txt"))
            {
                for (int i = 0; i < origin_matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < origin_matrix.GetLength(1); j++)
                    {
                        sw.Write($"{origin_matrix[i, j]} ");
                    }
                    sw.WriteLine();
                }
            }


            using Process process = Process.Start(new ProcessStartInfo
            {
                FileName = "python",
                Arguments = @"path\test.py",
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true
            });
            System.Threading.Thread.Sleep(1000);

            int ammountOfLines = 0;
            using (StreamReader sr = new StreamReader(@"eigenvalues.txt"))
            {
                String line;
                
                while ((line = sr.ReadLine()) != null)
                    ammountOfLines++;
                
            }
            double[] eigenvalues = new double[ammountOfLines];
            using (StreamReader sr = new StreamReader(@"eigenvalues.txt"))
            {
                string line;
                int i = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    line = Replacer(line);
                    double value = Convert.ToDouble(line);
                    eigenvalues[i] = Math.Round(value, 4);
                    i++;
                }
            }
            
            for (int i = 0; i < eigenvalues.Length; i++)
            {
                for (int j = 0; j < eigenvalues.Length - 1; j++)
                {
                    if (eigenvalues[j] < eigenvalues[j + 1])
                    {
                        double z = eigenvalues[j];
                        eigenvalues[j] = eigenvalues[j + 1];
                        eigenvalues[j + 1] = z;
                    }
                }
            }
            return eigenvalues;
        }

        private string Replacer(string line)
        {
            char[] array = line.ToCharArray(0, line.Length);
            for(int i = 0; i < line.Length; i++)
            {
                if (array[i] == '.')
                    array[i] = ',';

            }
            string newline = new string(array);
            return newline;
        }

        private double[] Eigenvector(double[,] matrix, double eigenvalue, int size_of_current_matrix) //надо доработать
        {
            double[,] curr_matrix = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for(int i = 0; i< matrix.GetLength(0); i++)
            {
                for(int j = 0; j < matrix.GetLength(1); j++)
                {
                    curr_matrix[i, j] = matrix[i, j];
                }
            }
            double[] vector = new double[size_of_current_matrix];
            for (int i = 0; i < curr_matrix.GetLength(0); i++)
            {
                curr_matrix[i, i] -= eigenvalue;
            }
            for (int i = 0; i < curr_matrix.GetLength(0) - 1; i++)
            {
                for (int j = i + 1; j < curr_matrix.GetLength(0); j++)
                {
                    double koefic = curr_matrix[j, i] / curr_matrix[i, i];
                    for (int k = i; k < curr_matrix.GetLength(0); k++)
                    {
                        curr_matrix[j, k] -= curr_matrix[i, k] * koefic;
                    }
                }
            }
            double koef =  - curr_matrix[curr_matrix.GetLength(0) - 2, curr_matrix.GetLength(0) - 2] / curr_matrix[curr_matrix.GetLength(0) - 2, curr_matrix.GetLength(0) - 1];
            vector[vector.Length - 1] = koef;
            vector[vector.Length - 2] =  - (curr_matrix[curr_matrix.GetLength(0) - 2, curr_matrix.GetLength(0) - 1] * koef) / curr_matrix[curr_matrix.GetLength(0) - 2, curr_matrix.GetLength(0) - 2];

            int vector_filled_values = 2;
            for(int i = curr_matrix.GetLength(0) - 3; i >= 0; i--)
            {
                int j = curr_matrix.GetLength(0) - 1;
                double sum = 0;
                for(int k = vector.Length - 1; k>= vector.Length - vector_filled_values; k--)
                {
                    sum += curr_matrix[i, j] * vector[k];
                    j--;
                }
                vector[vector.Length - vector_filled_values - 1] = -(sum / curr_matrix[i, i]);
                vector_filled_values++;
            }
            double normalized_koef = 0;
            for (int i = 0; i < vector.Length; i++)
                normalized_koef += Math.Pow(vector[i], 2);
            for (int i = 0; i < vector.Length; i++)
                vector[i] = vector[i] / Math.Sqrt(normalized_koef);

            return vector;

        }
        public double[] Singular_Values(double[] eignvalues)
        {
            for(int i = 0; i < eignvalues.Length; i++)
            {
                eignvalues[i] = Math.Round(Math.Sqrt(eignvalues[i]), 4);
            }
            return eignvalues;
        }
        public double[,] Sigma(double[,] origin_matrix, double[] eignvalues)
        {
            double[] singular_values = Singular_Values(eignvalues);
            double[,] S = new double[origin_matrix.GetLength(0),origin_matrix.GetLength(1)];

            int count_of_singular_values = 0;
            for(int i = 0; i < S.GetLength(0); i++)
            {
                for(int j = 0; j < S.GetLength(1); j++)
                {
                    if(i == j)
                    {
                        S[i, j] = singular_values[count_of_singular_values];
                        count_of_singular_values++;
                    }
                    if (count_of_singular_values == singular_values.Length)
                        break;
                }
            }

            return S;
        }
        public double[,] V_transposed(double[,] origin_matrix, double[] eignvalues, double[,] AtA)
        {
            double[,] V_transposed = new double[origin_matrix.GetLength(1), origin_matrix.GetLength(1)];

            int count_of_eignvalues = 0;
            int i = 0;
            while( i < V_transposed.GetLength(0))
            {

                double[] vector = Eigenvector(AtA, eignvalues[count_of_eignvalues], V_transposed.GetLength(1));
                for(int j = 0; j < V_transposed.GetLength(1); j++)
                {
                    V_transposed[i, j] = vector[j];
                }
                i++;
                count_of_eignvalues++;
            }

            return V_transposed;
        }

        public double[,] U_matrix(double[,] origin_matrix, double[] eignvalues, double[,] AAt)
        {
            double[,] u_matrix = new double[origin_matrix.GetLength(0), origin_matrix.GetLength(0)];

            int count_of_eignvalues = 0;
            int i = 0;
            while (i != u_matrix.GetLength(0))
            {
                double[] vector = Eigenvector(AAt, eignvalues[count_of_eignvalues], u_matrix.GetLength(1));
                for (int j = 0; j < u_matrix.GetLength(1); j++)
                {
                    u_matrix[i, j] = vector[j];
                }
                i++;
                count_of_eignvalues++;
            }

            u_matrix = Matrix_Transpose(u_matrix);

            return u_matrix;
        }
        public void Decomposition(double[,] matrix, out double[,] U, out double[,] S, out double[,] Vt)
        {
            //NumPyInstallation npi = new NumPyInstallation();
            //Task task = new Task(npi.Task);
            //task.Start();
            //task.Wait();

            //List<double[,]> results_of_decomposition = new List<double[,]>();

            double[,] transposed_matrix = Matrix_Transpose(matrix);
            double[,] AtA = Matrix_multiplier(transposed_matrix, matrix); //матрица для S и Vt
            
            double[] eigenvalues = Eigenvalues_ascending(AtA); //собственные значения для S и Vt 
            Vt = V_transposed(matrix, eigenvalues, AtA);
            S = Sigma(matrix, eigenvalues);

            double[,] AAt = Matrix_multiplier(matrix, transposed_matrix);
            eigenvalues = Eigenvalues_ascending(AAt);
            U = U_matrix(matrix, eigenvalues, AAt);


            //results_of_decomposition.Add(U);            
            //results_of_decomposition.Add(S); 
            //results_of_decomposition.Add(Vt);


        }
    }
}
