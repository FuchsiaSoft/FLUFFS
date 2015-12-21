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
using System.Runtime.InteropServices;
using GetDocText.Native;

namespace GetDocText.Ole
{
	internal class OleStream
	{
		UCOMIStream _Stream;
		string _Name;

		// constructors...
		public OleStream(UCOMIStream stream, string name)
		{
			_Stream = stream;
			_Name = name;
		}

		// protected methods...
		protected void Dispose(bool isDisposing)
		{
			if (_Stream == null)
				return;
			if (isDisposing)
			{
				_Stream.Commit(0);
			}
			Marshal.ReleaseComObject(_Stream);
			_Stream = null;
		}

		// public methods...
    /// <summary>
    /// Disposes the stream ...
    /// </summary>
    public void Dispose()
    {
      if (!IsDisposed)
      {
        GC.SuppressFinalize(this);
        Dispose(true);
      }
    }
		/// <summary>
		/// Reads all stream data.
		/// </summary>
		public byte[] ReadToEnd()
		{
			STATSTG stat;
			_Stream.Stat(out stat, 0);
			long size = stat.cbSize;
			byte[] buffer = new byte[size];
			_Stream.Read(buffer, (int)size, IntPtr.Zero);
			return buffer;
		}
		/// <summary>
		/// Writes the specified bytes into the stream.
		/// </summary>
		/// <param name="data">The data to write.</param>
		public void Write(byte[] data)
		{
			_Stream.Write(data, data.Length, IntPtr.Zero);
		}
    /// <summary>
    /// Closes the stream.
    /// </summary>
    public void Close()
    {
      Dispose();
    }

		// public properties...
		public string Name
		{
			get
			{
				return _Name;
			}
		}
		public bool IsDisposed
		{
			get
			{
				return _Stream == null;
			}
		}
	}
}