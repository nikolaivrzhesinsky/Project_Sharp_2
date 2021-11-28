using System;
using MySql.Data.MySqlClient;


namespace SQLtest
{
    class Program
    {
        DataBase obj1 = new DataBase();
        static void Main(string[] args)
        {
            Console.WriteLine("Getting Connection ...");
            MySqlConnection connection = DataBase.GetDB();
           

            try
            {
                Console.WriteLine("Openning Connection ...");

                connection.Open();

                Console.WriteLine("Connection successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            Console.Read();
        }
    }
}
