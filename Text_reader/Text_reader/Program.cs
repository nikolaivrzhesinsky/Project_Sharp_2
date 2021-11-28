using System;
using System.Collections.Generic;
using System.IO;
using Library;
using System.Data;
using MySql.Data.MySqlClient;

namespace Text_reader
{
    
    struct Frequency
    {
        public string word;
        public int frequence;
        public Frequency(string word, int frequency)
        {
            this.word = word;
            this.frequence = frequency;
        }
    }

    class Program
    {
        public static List<Frequency> GetFrequency(List<string> words)
        {
            DataBase db = new DataBase();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            List<Frequency> frequenceList = new List<Frequency>();

            for(int i=0; i < words.Count; i++)
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `word`= @w", db.getConnection());
                command.Parameters.Add("@w", MySqlDbType.VarChar).Value = words[i];
                db.openConnection();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    db.closeConnection();
                    string word = words[i];
                    int frequency = 0;
                    foreach(string j in words)
                    {
                        if(j == word)
                        {
                            frequency++;
                            words.RemoveAt(words.IndexOf(j));
                        }
                    }
                    Frequency f = new Frequency(word, frequency);
                    frequenceList.Add(f);
                }
                else
                {
                    db.closeConnection();

                    
                }
            }
            
            return frequenceList;
        }
        
        static void Main(string[] args)
        {
            

            List<string> words = new List<string>();
            string path = @"C:\Users\HYPERPC\Desktop\words.txt";

            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            using (StreamReader sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    string[] array = sr.ReadLine().Split();
                    for(int i = 0; i < array.Length; i++)
                    {
                        words.Add(array[i]);
                    }
                }

            }
            foreach (string i in words)
            {
                Console.WriteLine(i);
            }


        }
    }
}
