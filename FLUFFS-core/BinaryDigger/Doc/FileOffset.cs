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

namespace GetDocText.Doc
{
  /// <summary>
  /// Represents offset in a file.
  /// </summary>
  internal class FileOffset
  {
    public static readonly int Size = 4;
    static readonly uint UnicodeMask = (uint)(1 << 30);
    uint value;

    /// <summary>
    /// Creates new instance of FileOffset.
    /// </summary>
    public FileOffset()
      : this (0)
    {
    }
    /// <summary>
    /// Creates new instance of FileOffset.
    /// </summary>
    public FileOffset(uint value)
    {
      this.value = value;
    }

    // public methods...
    /// <summary>
    /// Reads file offset from the given binary reader.
    /// </summary>
    /// <param name="reader">The binary reader to use.</param>
    public void Read(BinaryReader reader)
    {
      value = reader.ReadUInt32();
    }
    /// <summary>
    /// Writes file offset using given binary writer.
    /// </summary>
    /// <param name="writer">The binary writer to use.</param>
    public void Write(BinaryWriter writer)
    {
      writer.Write(value);
    }

    // public static methods...
    /// <summary>
    /// Returns true if the file offset is unicode.
    /// </summary>
    /// <param name="fc">The file offset to test.</param>
    public static bool IsUnicode(uint fc)
    {
      return (fc & UnicodeMask) == 0;
    }
    /// <summary>
    /// Normalizes given file offset.
    /// </summary>
    /// <param name="fc">The file offset to normalize.</param>
    public static uint NormalizeFc(uint fc)
    {
      if ((fc & UnicodeMask) == 0)
        return fc;
      return (fc & ~UnicodeMask) / 2;
    }   
    /// <summary>
    /// Returns file offset delta.
    /// </summary>
    /// <param name="isUnicode">The value determining if char is unicode.</param>
    public static uint GetFcDelta(bool isUnicode)
    {
      return isUnicode ? (byte)2 : (byte)1;
    }   
    
    // public properties...
    /// <summary>
    /// Gets or sets file offset value.
    /// </summary>
    public uint Value
    {
      get
      {
        return this.value;
      }
      set
      {
        this.value = value;
      }
    }    
  }
}