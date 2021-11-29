﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SearchingLibrary
{
    public class DataBase
    {
        static string connectionString = "server=localhost;user=root;database=testc#;password=8616;";
        MySqlConnection connection = new MySqlConnection(connectionString);

        public MySqlConnection getConection()
        {
            return connection;
        }

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

    }
}
