using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FerretClientUI.Utils
{
    public enum CaseOption
    {
        AllCaps,
        TitleCase,
        LowerCase,
        RandomCase
    }

    public enum NumberOption 
    {
        NoNumbers, 
        AllNumbers, 
        /// <summary>
        /// A random number of characters will be replaced by random numbers
        /// </summary>
        NumbersInWords,
        /// <summary>
        /// A random number of words will be replaced entirely with numbers
        /// </summary>
        NumbersAsWords
    }

    public static class RandomWordGenerator
    {
        private static List<string> _WordList = GetWords();
        private static Random random = new Random();

        public static string GenerateRandomText(int wordCount, CaseOption caseOption,
            NumberOption numberOption)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < wordCount; i++)
            {
                string thisWord = GetRandomWord(caseOption, numberOption);
                builder.Append(thisWord);
                builder.Append(GetPunctuation());
            }

            return builder.ToString();
        }

        private static string GetRandomWord(CaseOption caseOption, NumberOption numberOption)
        {
            string word = _WordList[random.Next(0, _WordList.Count)];

            switch (numberOption)
            {
                case NumberOption.AllNumbers:
                    word = ReplaceAllWithNumbers(word);
                    break;

                case NumberOption.NumbersAsWords:
                    int threshold = random.Next(1, 50);
                    int probability = random.Next(1, 100);
                    if (probability < threshold)
                    {
                        word = ReplaceAllWithNumbers(word);
                    }
                    break;

                case NumberOption.NumbersInWords:
                    word = ReplaceSomeWithNumbers(word);
                    break;
            }

            switch (caseOption)
            {
                case CaseOption.AllCaps:
                    word = word.ToUpper();
                    break;

                case CaseOption.LowerCase:
                    word = word.ToLower();
                    break;

                case CaseOption.TitleCase:
                    TextInfo ti = new CultureInfo("en-US", false).TextInfo;
                    word = ti.ToTitleCase(word);
                    break;

                case CaseOption.RandomCase:
                    word = RandomCase(word);
                    break;
            }

            return word;
        }

        private static string ReplaceAllWithNumbers(string word)
        {
            string newWord = "";
            for (int i = 0; i < word.Length - 1; i++)
            {
                newWord += random.Next(0, 10).ToString();
            }
            return newWord;
        }

        private static string ReplaceSomeWithNumbers(string word)
        {
            if (random.Next(1, 100) < random.Next(1, 20)) return word;
            string newWord = "";
            int threshold = random.Next(20, 60);
            char[] letters = word.ToCharArray();
            foreach (char letter in letters)
            {
                if (random.Next(1, 100) < threshold)
                {
                    newWord += random.Next(0, 10).ToString();
                }
                else
                {
                    newWord += letter.ToString();
                }
            }
            return newWord;
        }

        private static string RandomCase(string word)
        {
            string newWord = "";
            int threshold = 50;
            char[] letters = word.ToCharArray();
            foreach (char letter in letters)
            {
                if (random.Next(1, 100) < threshold)
                {
                    newWord += letter.ToString().ToUpper();
                }
                else
                {
                    newWord += letter.ToString().ToLower();
                }
            }
            return newWord;
        }

        private static string GetPunctuation()
        {
            string punctuation = "";
            int probability = random.Next(1, 100);
            if (probability < 10)
            {
                if (probability >= 1 && probability < 7)
                {
                    punctuation = ", ";
                }
                else
                {
                    punctuation = ".  ";
                    if (probability == 8)
                    {
                        punctuation += Environment.NewLine + Environment.NewLine;
                    }
                }
            }
            else
            {
                punctuation = " ";
            }
            return punctuation;
        }

        private static List<string> GetWords()
        {
            return Properties.Resources.wordsEn.Split(Environment.NewLine.ToCharArray()).ToList();
        }
    }
}
