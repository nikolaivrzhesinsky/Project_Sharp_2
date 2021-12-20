using FirstStep_Library;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstStep_Library
{
    public class Freq
    {
        static DirectoryInfo di = new DirectoryInfo(@"texts_for_analysis");
        public static int N = di.GetFiles().Length;
        public string word;
        public int[] frequence = new int[N];
        public Freq(string word, int[] frequence)
        {
            this.word = word;
            this.frequence = frequence;
        }
    }

    public class FrequencyAndSentimentAnalys
    {
        List<Freq> freqList = new List<Freq>();
        List<string> word_list = new List<string>();

        private bool isInTheList(List<Freq> freqList, string word)
        {

            for (int k = 0; k < freqList.Count; k++)
            {
                if (freqList[k].word == word)
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
            sql = "SELECT MAX(idlibrary) FROM library";
            int count = data.CountBD(data.getConection(), sql);
            //MySqlDataAdapter adapter = new MySqlDataAdapter();

            for (int i = 0; i < text.Count; i++)
            {
                data.openConnection();

                for (int k = 1; k <= count; k++)
                {
                    data.openConnection();
                    sql = "SELECT word FROM library WHERE idlibrary = " + k;
                    command.Connection = data.getConection();
                    command.CommandText = sql;
                    string dicWord = command.ExecuteScalar().ToString();

                    if (isPrefixSufix(text[i], dicWord))
                    {
                        data.closeConnection();

                        string word = dicWord;
                        int[] tempFreq = new int[Freq.N];


                        if (isInTheList(freqList, word))
                        {
                            for (int j = 0; j < text.Count; j++)
                            {
                                //text[j] == word
                                if (isPrefixSufix(text[j], word))
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
                        break;
                    }
                    else
                    {
                        data.closeConnection();

                    }
                }

            }
            return freqList;
        }

        /*public void ShowFreq()
        {
            for(int i = 0; i< freqList.Count; i++)
            {
                Console.Write($" {freqList[i].word}| ");
                for (int j =0; j< freqList[i].frequence.Length; j++)
                {
                    Console.Write($"{freqList[i].frequence[j]} ");
                }
                Console.WriteLine("\n");
            }
        }*/

        static DirectoryInfo di2 = new DirectoryInfo(@"texts_for_analysis");
        public static int N = di2.GetFiles().Length;
        public int[,] FreqMatrix;

        public double[,] MakeFreqMatrix()
        {
            double[,] FreqMatrix = new double[freqList.Count, N];
            for (int i = 0; i < freqList.Count; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    FreqMatrix[i, j] = freqList[i].frequence[j];
                }
            }
            return FreqMatrix;
        }

        public List<string> Word_List()
        {
            for (int i = 0; i < freqList.Count; i++)
            {
                word_list.Add(freqList[i].word);
            }

            return word_list;
        }

        /*public void ShowFreqMatrix()
        {
            int height = FreqMatrix.GetLength(0);
            int width = FreqMatrix.GetLength(1);
            for(int i = 0; i < height; i++)
            {
                for(int j=0;j<width;j++)
                {
                    Console.Write($"{FreqMatrix[i,j]} ");
                }
                Console.WriteLine("\n");
            }
        }*/


        public List<int> SentResult = new List<int>();
        int SumSent = 0;
        public void GetSentimantAnalys(List<string> text)
        {
            DataBase data = new DataBase();
            sql = "SELECT MAX(idwords_raiting) FROM words_raiting";
            int count = data.CountBD(data.getConection(), sql);

            for (int i = 0; i < text.Count; i++)
            {
                data.openConnection();

                for (int k = 1; k <= count; k++)
                {

                }
            }
        }
    }
}
