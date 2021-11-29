using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace SearchingLibrary
{
    public struct Freq
    {
        public string word;
        public int frequence;
        public Freq(string word, int frequence)
        {
            this.word = word;
            this.frequence = frequence;
        }
    }

    public class Frequency
    {

        private List<Freq> GetFrequency(List<string> text)
        {
            DataBase data = new DataBase();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            
            List<Freq> freqList = new List<Freq>();

            for (int i = 0; i < text.Count; i++)
            {
                data.openConnection();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `words` WHERE `word`= @w", data.getConection());
                command.Parameters.Add("@w", MySqlDbType.VarChar).Value = text[i];
                adapter.SelectCommand = command;
                DataTable table = new DataTable();
                adapter.Fill(table);
                

                if (table.Rows.Count > 0)
                {
                    data.closeConnection();
                    
                    string word = text[i];
                    int frequency = 0;
                    for(int j = 0; j< text.Count; j++)
                    {
                        if (text[j] == word)
                        {
                            frequency++;
                            text.RemoveAt(text.IndexOf(text[j]));
                            j--;
                            
                        }
                    }
                    Freq f = new Freq(word, frequency);
                    freqList.Add(f);
                    i--;
                }
                else
                {
                    data.closeConnection();
                    
                }

                
            }
            return freqList;
        }

        public void ShowFreq(List<string> text)
        {
            var objText = GetFrequency(text);
            Console.WriteLine("Слово.............Частота");
            for(int i = 0; i < objText.Count; i++)
            {
                string tWord = objText[i].word+"......."+objText[i].frequence;
                Console.WriteLine(tWord) ;
            }
        }
     


    }
}
