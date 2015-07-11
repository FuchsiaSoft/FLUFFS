using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerretClientUI.Utils
{
    internal enum CaseOption
    {
        AllCaps,
        TitleCase,
        LowerCase,
        RandomCase
    }

    internal enum NumberOption 
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

    internal static class RandomWordGenerator
    {
        static string GenerateRandomText(int wordCount)
        {
            return string.Empty;
        }
    }
}
