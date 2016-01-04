#region Copyright (c) 2006-2008 Cellbi
/*
Cellbi Software Component Product
Copyright (c) 2006-2008 Cellbi
www.cellbi.com

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

	1.	Redistributions of source code must retain the above copyright notice,
			this list of conditions and the following disclaimer.

	2.	Redistributions in binary form must reproduce the above copyright notice,
			this list of conditions and the following disclaimer in the documentation
			and/or other materials provided with the distribution.

	3.	The names of the authors may not be used to endorse or promote products derived
			from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED “AS IS” AND ANY EXPRESSED OR IMPLIED WARRANTIES,
INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL CELLBI
OR ANY CONTRIBUTORS TO THIS SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT,
INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA,
OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion

using System;
using System.IO;

using GetDocText.Native;
using GetDocText.Ole;
using System.Text;

namespace GetDocText.Doc
{
    /// <summary>
    /// Implements loading of the text from the doc files.
    /// </summary>
    public class TextLoader
    {
        string _Path;

        // constructors ...
        /// <summary>
        /// Creates new instance of the TextLoader.
        /// </summary>
        /// <param name="path">The path of the file to load the text</param>
        public TextLoader(string path)
        {
            _Path = path;
        }

        // private methods ...
        BinaryReader GetReader(OleStream stream)
        {
            if (stream == null)
                return null;
            byte[] streamData = stream.ReadToEnd();
            MemoryStream memoryStream = new MemoryStream(streamData);
            return new BinaryReader(memoryStream);
        }
        BinaryReader GetStreamReader(OleStorage storage, string streamName)
        {
            OleStream stream = storage.OpenStream(streamName);
            if (stream == null)
                return null;
            return GetReader(stream);
        }
        BinaryReader GetDocumentStreamReader(OleStorage storage)
        {
            return GetStreamReader(storage, "WordDocument");
        }
        BinaryReader GetTableStreamReader(OleStorage storage, string tableName)
        {
            return GetStreamReader(storage, tableName);
        }
        void GetDataFromFib(BinaryReader reader, out string tableName, out int pdcOffset, out uint pdcLength)
        {
            reader.BaseStream.Seek(10, SeekOrigin.Begin);
            ushort flags = reader.ReadUInt16();
            tableName = BitUtils.IsSet(flags, 9) ? "1Table" : "0Table";

            reader.BaseStream.Seek(418, SeekOrigin.Begin);
            pdcOffset = reader.ReadInt32();
            pdcLength = reader.ReadUInt32();
        }
        PieceDescriptorCollection GetPieceDescriptors(BinaryReader reader, int offset, uint length)
        {
            PieceDescriptorCollection result = new PieceDescriptorCollection(offset, length);
            result.Read(reader);
            return result;
        }
        string ReadString(BinaryReader reader, uint length, bool isUnicode)
        {
            if (length == 0)
                return string.Empty;

            if (isUnicode)
                length = length / 2;

            //EDIT: This loop gets called several million times in a large
            //document, so moving from string concatenation using operator
            //overloads to a stringbuilder for performance, this will also
            //get rid of our casts which will be getting called
            //millions of times.
            //Chris Wilson - 04/01/16

            StringBuilder builder = new StringBuilder((int)length,(int)length);

            string result = string.Empty;

            for (int i = 0; i < length; i++)
            {
                if (!isUnicode)
                {
                    byte ch = reader.ReadByte();

                    //CHANGED AS PER ABOVE

                    builder.Append((char)ch);

                    //result += (char)ch;
                }
                else
                {
                    short ch = reader.ReadInt16();

                    builder.Append((char)ch);

                    //result += (char)ch;
                }
            }

            result = builder.ToString();

            return result;
        }
        bool LoadText(OleStorage storage, out string text)
        {
            text = string.Empty;
            if (storage == null)
                return false;

            BinaryReader documentReader = GetDocumentStreamReader(storage);
            if (documentReader == null)
                return false;

            int pdcOffset;
            uint pdcLength;
            string tableName;
            GetDataFromFib(documentReader, out tableName, out pdcOffset, out pdcLength);

            BinaryReader tableReader = GetTableStreamReader(storage, tableName);
            if (tableReader == null)
                return false;

            PieceDescriptorCollection pieces = GetPieceDescriptors(tableReader, pdcOffset, pdcLength);
            if (pieces == null)
                return false;

            //EDIT: Chris Wilson 04/01/16
            //We have had some performance issues with legacy word docs, and I suspect that
            //it's the use of overloaded operator concatenation in the below loop
            //rather than using a stringbuilder
            //change is to address this speed issue as we are encountering documents with word counts
            //in the several 100ks

            //ADDED: as per comment above
            StringBuilder builder = new StringBuilder();

            int count = pieces.Count;
            for (int i = 0; i < count; i++)
            {
                uint pieceStart;
                uint pieceEnd;
                bool isUnicode = pieces.GetPieceFileBounds(i, out pieceStart, out pieceEnd);

                documentReader.BaseStream.Seek(pieceStart, SeekOrigin.Begin);

                //REMOVED: Chris Wilson as per comment above
                //text += ReadString(documentReader, pieceEnd - pieceStart, isUnicode);

                //ADDED: Chris Wilson as per comment above
                builder.Append(ReadString(documentReader, pieceEnd - pieceStart, isUnicode));

            }

            //ADDED: Chris Wilson as per comment above
            text = builder.ToString();

            return true;
        }

        // public methods ...
        /// <summary>
        /// Loads text from the file.
        /// </summary>
        /// <param name="text">The text of the file</param>
        public bool LoadText(out string text)
        {
            text = string.Empty;
            if (NativeMethods.StgIsStorageFile(_Path) != 0)
                return false;

            OleStorage storage = OleStorage.CreateInstance(_Path);
            try
            {
                return LoadText(storage, out text);
            }
            finally
            {
                storage.Close();
            }
        }
    }
}
