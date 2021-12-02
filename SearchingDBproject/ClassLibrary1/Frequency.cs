using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;
using System.Globalization;


namespace SearchingLibrary
{
    public class Freq
    {
        static DirectoryInfo di = new DirectoryInfo(@"C:\Users\абв\Documents\GitHub\Project_Sharp_2\SearchingDBproject\Тексты для АНАЛиза");
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

        private bool isPrefixSufix(string word, string DBword)
        {
            CompareInfo myComp = CultureInfo.InvariantCulture.CompareInfo;
            bool v1 = myComp.IsPrefix(word, DBword);
            bool v2 = myComp.IsSuffix(word, DBword);
            if (v1 == true || v2 == true)
            {
                return true;
            }
            else return false;
        }

        private MySqlCommand command = new MySqlCommand();
        private string sql = "";
        public List<Freq> GetFrequency(List<string> text, int textNumber)
        {
            DataBase data = new DataBase();
            int count = data.CountBD(data.getConection());      
            //MySqlDataAdapter adapter = new MySqlDataAdapter();

            for (int i = 0; i < text.Count; i++)
            {
                data.openConnection();
               
                /*  MySqlCommand command = new MySqlCommand("SELECT * FROM `words` WHERE `word`= @w", data.getConection());
                  command.Parameters.Add("@w", MySqlDbType.VarChar).Value = text[i];
                  adapter.SelectCommand = command;
                  DataTable table = new DataTable();
                  adapter.Fill(table);
                  */
                for (int k = 1; k <= count; k++)
                {
                    data.openConnection();
                    sql = "SELECT word FROM library WHERE idlibrary = " + k;
                    command.Connection = data.getConection();
                    command.CommandText = sql;
                    string dicWord = command.ExecuteScalar().ToString();

                    if (isPrefixSufix(text[i],dicWord))
                    {
                        data.closeConnection();

                        string word = dicWord;
                        int[] tempFreq = new int[Freq.N];


                        if (isInTheList(freqList, word))
                        {
                            for (int j = 0; j < text.Count; j++)
                            {
                                //text[j] == word
                                if (isPrefixSufix(text[j],word))
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
                                if (isPrefixSufix(text[j], word))
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
