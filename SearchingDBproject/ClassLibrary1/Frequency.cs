using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;


namespace SearchingLibrary
{
    public class Freq
    {
        static DirectoryInfo di = new DirectoryInfo(@"C:\Users\HYPERPC\Desktop\texts");
        public static int N = di.GetFiles().Length;
        public string word;
        public int[] frequence = new int [N];
        public Freq(string word, int[] frequence)
        {
            this.word = word;
            this.frequence = frequence;
        }
       
    }

    public class Frequency
    {
        List<Freq> freqList = new List<Freq>();

        private bool isInTheList(List<Freq> freqList, string word)
        {
            
            for(int k = 0; k < freqList.Count; k++)
            {
                if(freqList[k].word == word)
                {
                    return true;
                }
            }
            return false;
        }
        private int GetIndex(List<Freq> freqList, string word)
        {
            for (int k = 0; k < freqList.Count; k++)
            {
                if (freqList[k].word == word)
                {
                    return k;
                }
            }
            return 0;
        }

        public List<Freq> GetFrequency(List<string> text, int textNumber)
        {
            DataBase data = new DataBase();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

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
                    int[] tempFreq = new int[Freq.N];
                    

                    if (isInTheList(freqList, word))
                    {
                        for (int j = 0; j < text.Count; j++)
                        {
                            if (text[j] == word)
                            {
                                freqList[GetIndex(freqList, word)].frequence[textNumber]++;
                                text.RemoveAt(text.IndexOf(text[j]));
                                j--;

                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < text.Count; j++)
                        {
                            if (text[j] == word)
                            {
                                tempFreq[textNumber]++;
                                text.RemoveAt(text.IndexOf(text[j]));
                                j--;

                            }
                        }
                        Freq f = new Freq(word, tempFreq);
                        freqList.Add(f);
                    }
                    
                    
                    i--;
                }
                else
                {
                    data.closeConnection();
                    
                }

                
            }
            return freqList;
        }

        public void ShowFreq()
        {
            for(int i = 0; i< freqList.Count; i++)
            {
                Console.WriteLine(freqList[i].word);
                for (int j =0; j< freqList[i].frequence.Length; j++)
                {
                    Console.WriteLine(freqList[i].frequence[j] + $" - Частота в {j+1} тексте");
                }

            }
        }

    }
}
