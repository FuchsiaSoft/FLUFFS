using GetDocText.Doc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryDigger
{

    /*
        This is a temporary solution for expedience of testing the pilot area
        
        The code for reading text from word files is facilitated by some open
        source code found here: http://www.codeproject.com/Articles/22738/Read-Document-Text-Directly-from-Microsoft-Word-Fi

        This will all be replaced with Word Binary Reader when completed, which
        is a more complete library looking at more than just body text
        from the character positioning system in binary files.
        
        For the purposes of this pilot we're not worrying about much and it'll
        just return null if it can't read the file

        Chris Wilson 18/12/2015

    */

    class WordReader : BinaryReader
    {
        public WordReader(string path)
        {
            _FilePath = path;
        }

        public override string ReadContents()
        {
            TextLoader loader = new TextLoader(_FilePath);

            string output;

            if (loader.LoadText(out output) == false)
            {
                return null;
            }

            return output;
        }
    }
}
