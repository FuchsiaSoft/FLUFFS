using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.FileReading
{
    static class TestConstants
    {
        /// <summary>
        /// Provides the strings that are known to exist in the sample files
        /// </summary>
        public static IList<string> StringsToCheck = new List<string>()
        {
            "Buonapartes",
            "Pdvlovna"
        };

        /// <summary>
        /// Provides the regexes that are known to match the text which
        /// is the same as those in _StringsToCheck
        /// </summary>
        public static IList<string> RegExToCheck = new List<string>()
        {
            @"(?i)(buo[na]+partes)",
            @"(?i)(pd[vlon]+a)"
        };
    }
}
