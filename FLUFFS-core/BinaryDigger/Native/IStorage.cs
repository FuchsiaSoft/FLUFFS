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

namespace GetDocText.Native
{
	[ComImport, ComConversionLoss, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid(Guids.IStorage)]
	internal interface IStorage
	{
		int CreateStream(string pwcsName, int grfMode, int reserved1, int reserved2, out UCOMIStream ppstm);
		int OpenStream(string pwcsName, IntPtr reserved1, int grfMode, int reserved2, out UCOMIStream ppstm);
		int CreateStorage(string pwcsName, int grfMode, int reserved1, int reserved2, out IStorage ppstg);
		int OpenStorage(string pwcsName, IStorage pstgPriority, int grfMode, tagRemSNB snbExclude, int reserved, out IStorage ppstg);
		int CopyTo(int ciidExclude, ref Guid rgiidExclude, ref tagRemSNB snbExclude, IStorage pstgDest);
		int MoveElementTo(string pwcsName, IStorage pstgDest, string pwcsNewName, int grfFlags);
		int Commit(int grfCommitFlags);
		int Revert();
		int EnumElements(int reserved1, IntPtr reserved2, int reserved3, out IEnumSTATSTG ppenum);
		int DestroyElement(string pwcsName);
		int RenameElement(string pwcsOldName, string pwcsNewName);
		int SetElementTimes(string pwcsName, ref FILETIME pctime, ref FILETIME patime, ref FILETIME pmtime);
		int SetClass(ref Guid clsid);
		int SetStateBits(int grfStateBits, int grfMask);
		int Stat(out STATSTG pstatstg, int grfStatFlag);
	}
}