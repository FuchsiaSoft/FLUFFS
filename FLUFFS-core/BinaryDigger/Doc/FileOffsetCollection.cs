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
using System.Collections;

namespace GetDocText.Doc
{
  /// <summary>
  /// Represents a collection of file offsets.
  /// </summary>
  internal class FileOffsetCollection : CollectionBase
  {
    /// <summary>
    /// Creates new instance of FileOffsetCollection.
    /// </summary>
    public FileOffsetCollection()
    {
    }

    // public methods...
    /// <summary>
    /// Adds file offset to the collection.
    /// </summary>
    /// <param name="offset">The offset to add.</param>
    public int Add(FileOffset offset)
    {
      return InnerList.Add(offset);
    }
    /// <summary>
    /// Adds file offset to the collection.
    /// </summary>
    /// <param name="offset">The offset to add.</param>
    public int Add(uint offset)
    {
      return Add(new FileOffset(offset));
    }
    /// <summary>
    /// Adds file offsets to the collection.
    /// </summary>
    /// <param name="offsets">The offsets to add.</param>
    public void AddRange(FileOffsetCollection offsets)
    {
      InnerList.AddRange(offsets);
    }
    /// <summary>
    /// Remove file offset from the collection.
    /// </summary>
    /// <param name="offset">The offset to remove.</param>
    public void Remove(FileOffset offset)
    {
      InnerList.Remove(offset);
    }   
    /// <summary>
    /// Clears all data and reads offsets from the specified reader.
    /// </summary>
    /// <param name="reader">The reader to use.</param>
    /// <param name="count">The number of offsets to read.</param>
    public void Read(BinaryReader reader, int count)
    {
      if (reader == null)
        return;
      Clear();
      for (int i = 0; i < count; i++)
      {
        FileOffset offset = new FileOffset();
        offset.Read(reader);
        Add(offset);
      }
    }
    /// <summary>
    /// Writes all data to the specified writer.
    /// </summary>
    /// <param name="writer">The writer to use.</param>
    public void Write(BinaryWriter writer)
    {
      if (writer == null)
        return;
      int count = Count;
      for (int i = 0; i < count; i++)
        this[i].Write(writer);
    }
    

    // public properties...
    /// <summary>
    /// Gets or sets file offset at the specified index.
    /// </summary>
    public FileOffset this[int index]
    {
      get
      {
        return (FileOffset)InnerList[index];
      }
      set
      {
        InnerList[index] = value;
      }
    }
  }
}