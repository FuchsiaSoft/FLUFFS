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
	internal class OleStorage : IDisposable
	{
		const int _DefaultFlags = (int)(STGMFlags.STGM_READWRITE | STGMFlags.STGM_SHARE_EXCLUSIVE);

		IStorage _Storage;
		string _Name;

    // constructors ...
		OleStorage(IStorage storage, string name)
		{
			if (storage == null)
				throw new ArgumentNullException("storage");
			
      _Storage = storage;
			_Name = name;
		}

    // destructor ...
    ~OleStorage()
    {
      Dispose(false);
    }		
		
		// private methods...    
		void Dispose(bool isDisposing)
		{
			if (_Storage == null)
				return;
			if (isDisposing)
			{
				_Storage.Commit(0);
			}
			Marshal.ReleaseComObject(_Storage);
			_Storage = null;
		}

		// public methods...
    /// <summary>
    /// Disposes the storage ...
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
    /// Creates new instance of the ole storage.
    /// </summary>
    /// <param name="path">The path of the file to create storage for</param>
    public static OleStorage CreateInstance(string path)
		{
      IStorage storage;
      int result = NativeMethods.StgOpenStorage(path, null, _DefaultFlags, IntPtr.Zero, 0, out storage);
      if (result != 0)
        return null;
      return new OleStorage(storage, "Root");
		}
    /// <summary>
    /// Closes the storage.
    /// </summary>
    public void Close()
    {
      Dispose();
    }		
		/// <summary>
		/// Opens stream with the given name.
		/// </summary>
		/// <param name="name">The name of the stream to open.</param>
		public OleStream OpenStream(string name)
		{
			UCOMIStream stream;
			int result = _Storage.OpenStream(name, IntPtr.Zero, _DefaultFlags, 0, out stream);
			if (result != 0)
				return null;
			return new OleStream(stream, name);
		}		
		/// <summary>
		/// Reads data from the specified stream.
		/// </summary>
		/// <param name="name">The name of the stream to read.</param>
		public byte[] ReadStream(string name)
		{
			OleStream stream = OpenStream(name);
			try
			{
				return stream.ReadToEnd();
			}
			finally
			{
				stream.Close();
			}
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
				return _Storage == null;
			}
		}
	}
}