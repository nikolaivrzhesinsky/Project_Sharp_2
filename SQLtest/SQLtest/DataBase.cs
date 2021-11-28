using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;


namespace SQLtest
{
    public class DataBase
    {
        static string host = "localhost";
        static int port = 3306;
        static string database = "users_tab";
        static string username = "root";
        static string password = "8616";

       
        public static MySqlConnection GetDB()
        {
             String connString = "Server=" + host + ";Database=" + database
             + ";port=" + port + ";User Id=" + username + ";password=" + password;
            MySqlConnection conn = new MySqlConnection(connString);
            return conn;
        }

        

    }
}
