using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace _1._4
{
    class Program
    {
        public static void ArrL(ArrayList AL)
        {
            var sw = Stopwatch.StartNew();
            //Скорость вставки элемента int:
            Console.WriteLine("Скорость вставки элемента int: ");
            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                AL.Insert(0, 1);
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            
            //Скорость получения элемента int:
            Console.WriteLine("Скорость получения элемента int: ");
            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                AL.IndexOf(1);
            }
                sw.Stop();
            Console.WriteLine(sw.Elapsed);
            
            //Скорость вставки элемента string:
            Console.WriteLine("Скорость вставки элемента string: ");
            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                AL.Insert(1, "The");
            }
                sw.Stop();
            Console.WriteLine(sw.Elapsed);

            //Скорость получения элемента string:
            Console.WriteLine("Скорость получения элемента string: ");
            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
               AL.IndexOf("The");
            }
                sw.Stop();
            Console.WriteLine(sw.Elapsed);
        }
        public static void ListINT(List<int> LI)
        {
             var sw1 = Stopwatch.StartNew();
              //Скорость вставки элемента int:
              Console.WriteLine("Скорость вставки элемента int для LIST: ");
              sw1.Start();
              for (int i = 0; i < 1000000; i++)
                  {
                      LI.Insert(0, 128);
                  }
                    sw1.Stop();
                    Console.WriteLine(sw1.Elapsed);
            
            //Скорость получения элемента int:
            Console.WriteLine("Скорость получения элемента int для LIST: ");
            sw1.Start();
            for (int i = 0; i < 1000000; i++)
            {
                LI.IndexOf(128);
            }
                sw1.Stop();
            Console.WriteLine(sw1.Elapsed);
        }
        public static void ListString(List<string> LS)
        {
            //Скорость вставки элемента string:
            var sw1 = Stopwatch.StartNew();
            Console.WriteLine("Скорость вставки элемента string для LIST: ");
            sw1.Start();
            for (int i = 0; i < 1000000; i++)
            {
                LS.Insert(i, "World");
            }
            sw1.Stop();
            Console.WriteLine(sw1.Elapsed);

            //Скорость получения элемента string:
            Console.WriteLine("Скорость получения элемента string для LIST: ");
            sw1.Start();
            for (int i = 0; i < 1000000; i++)
            {
                LS.IndexOf("World");
            }
            sw1.Stop();
            Console.WriteLine(sw1.Elapsed);
        }

        //Generic метод
        private static void Test<T>(T element, int indOP)
        {
            switch(indOP)
            {
                case 1:
                    {
                        var swT = Stopwatch.StartNew();
                        List<T> TestList = new List<T>();
                        swT.Start();
                        for (Int32 n = 0; n < 1000000; n++)
                        {
                            TestList.Insert(n, element);
                        }
                        swT.Stop();
                        Console.WriteLine("List: ");
                        Console.WriteLine(swT.Elapsed);
                        TestList = null; 

                        ArrayList Test = new ArrayList();
                        swT.Start();
                        for (Int32 n = 0; n < 1000000; n++)
                        {
                            Test.Insert(n, element);
                        }
                        swT.Stop();
                        Console.WriteLine("ArrayList: ");
                        Console.WriteLine(swT.Elapsed);
                        Test = null;
                      
                    } break;
                case 2:
                    {
                        var swT = Stopwatch.StartNew();
                        List<T> TestList = new List<T>();
                        for (Int32 n = 0; n < 1000000; n++)
                        {
                            TestList.Insert(n, element);
                        }
                        ArrayList Test = new ArrayList();
                        for (Int32 n = 0; n < 1000000; n++)
                        {
                            Test.Insert(n, element);
                        }

                        swT.Start();
                        for (Int32 n = 0; n < 1000000; n++)
                        {
                            TestList.IndexOf(element);
                        }
                        swT.Stop();
                        Console.WriteLine("List: ");
                        Console.WriteLine(swT.Elapsed);
                        TestList = null;

                        swT.Start();
                        for (Int32 n = 0; n < 1000000; n++)
                        {
                            Test.IndexOf(element);
                        }
                        swT.Stop();
                        Console.WriteLine("ArrayList: ");
                        Console.WriteLine(swT.Elapsed);
                        Test = null;
                    }
                    break;
            }   
         }
            static void Main(string[] args)
        {
                            //ArrayList 
            Console.WriteLine("     Работа с ArrayList: ");
            ArrayList list1 = new ArrayList();
            ArrL(list1);
            //test<ArrayList>(list1);
            
                            //List int
            Console.WriteLine("     Работа с List: ");
            List<int> list2 = new List<int> { };
            ListINT(list2);
           
                        //List string
            List<string> list3 = new List<string> { };
            ListString(list3);

            //Genetic
            int a = 1;
            string b = "1";

            Console.WriteLine("\nСкорость вставки элемента int:");
            Test<int>(a, 1);
            Console.WriteLine("\nСкорость вставки элемента string:");
            Test<string>(b, 1);

            Console.WriteLine("\nСкорость получения элемента int: ");
            Test<int>(a, 2);
            Console.WriteLine("\nСкорость получения элемента string:");
            Test<string>(b, 2);
        }
    }
}
