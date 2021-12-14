﻿using System;
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

    public class FrequencyAndSentimentAnalys
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

        private bool isPrefix(string word, string DBword)
        {
            CompareInfo myComp = CultureInfo.InvariantCulture.CompareInfo;
            bool v1 = myComp.IsPrefix(word, DBword);
            if (v1 == true )
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
            int count = data.CountBD(data.getConection(),sql);      
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

        public void ShowFreq()
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
        }

        static DirectoryInfo di2 = new DirectoryInfo(@"C:\Users\абв\Documents\GitHub\Project_Sharp_2\SearchingDBproject\Тексты для АНАЛиза");
        public static int N = di2.GetFiles().Length;
        public int[,] FreqMatrix;

        public int[,] MakeFreqMatrix()
        {
            FreqMatrix = new int[freqList.Count, N];
            for (int i = 0; i < freqList.Count; i++)
            {
               for(int j = 0; j < N; j++)
                {
                    FreqMatrix[i, j] = freqList[i].frequence[j];
                }
            }
            return FreqMatrix;
        }

        public void ShowFreqMatrix()
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
        }

       
        public List<float> SentResult = new List<float>();
        private int counterSentWords = 0;
        private float SumSent = 0;
        private float meanInt = 0;
        public List<float> GetSentimantAnalys(List<string>text, int j)
        {
            DataBase data = new DataBase();
            sql = "SELECT MAX(idwords_raiting) FROM words_raiting";
            int countDB = data.CountBD(data.getConection(), sql);

            for (int i = 0; i < text.Count; i++)
            {
                data.openConnection();

                for (int k = 1; k <= countDB; k++)
                {
                    sql = "SELECT word FROM words_raiting WHERE idwords_raiting = " + k;
                    command.Connection = data.getConection();
                    command.CommandText = sql;
                    string dicWord = command.ExecuteScalar().ToString();

                    if (isPrefix(text[i], dicWord))
                    {
                       
                        sql= "SELECT mean FROM words_raiting WHERE idwords_raiting = " + k;
                        command.Connection = data.getConection();
                        command.CommandText = sql;
                        string mean= command.ExecuteScalar().ToString();
                        meanInt = float.Parse(mean);
                        SumSent += meanInt;
                        counterSentWords++;
                        k = 1;
                        break;
                    }
                    
                }
                data.closeConnection();
            }
            SentResult.Add(SumSent / counterSentWords);
            SumSent = 0;
            counterSentWords = 0;

            return SentResult;

        }

        public void ShowSentAnalys()
        {
            for(int i = 0; i < SentResult.Count; i++)
            {
                if(SentResult[i]>=-1 && SentResult[i] <= 1)
                {
                    Console.WriteLine($"Текст {i+1} имеет тональность \"нейтральная\""); 
                }
                if (SentResult[i] < -1)
                {
                    Console.WriteLine($"Текст {i+1} имеет тональность \"отрицательная\"");
                }
                if (SentResult[i] > 1)
                {
                    Console.WriteLine($"Текст {i+1} имеет тональность \"положительная\"");
                }
            }
        }

    }

   


   
}
