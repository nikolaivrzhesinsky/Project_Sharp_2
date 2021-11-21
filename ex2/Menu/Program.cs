using System;
using GeometryLibrary;
using ExceptionsLibrary;


namespace ex2

{
    class Program
    {

        static void Main(string[] args)
        {
            
            bool stream = true;

            while(stream)
            {
                Console.WriteLine("1-создать круг\n");
                Console.WriteLine("2-создать треугольник\n");
                Console.WriteLine("3-создать квадрат\n");
                Console.WriteLine("0-завершить работу\n");

                int switch_on = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\n");
                switch (switch_on)
                {
                    case 1:
                        {
                            Console.Clear();
                            Console.WriteLine("Введите радиус: ");
                            int rad = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                Circle circle = new Circle(rad);
                            }
                            catch (CircleException ex)
                            {

                                ex.FileWriter();
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            Console.WriteLine("Введите длины 3 сторон: ");
                            int side1 = Convert.ToInt32(Console.ReadLine());
                            int side2 = Convert.ToInt32(Console.ReadLine());
                            int side3 = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                Triangle triangle = new Triangle(side1, side2, side3);
                            }
                            catch (TriangleException ex)
                            {

                                ex.FileWriter();
                            }
                            break;
                        }
                    case 3:
                        {
                            Console.Clear();
                            Console.WriteLine("Введите длины 4 сторон: ");
                            int side1 = Convert.ToInt32(Console.ReadLine());
                            int side2 = Convert.ToInt32(Console.ReadLine());
                            int side3 = Convert.ToInt32(Console.ReadLine());
                            int side4 = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                Quadrangle triangle = new Quadrangle(side1, side2, side3, side4);
                            }
                            catch (QuadrangleException ex)
                            {

                                ex.FileWriter();
                            }
                            break;
                        }
                    case 0:
                        {
                            Console.Clear();
                            stream = false;
                            break;
                        }
                }
            }
            
        }
    }
}
