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

namespace GetDocText.Native
{
	[Flags]
	internal enum STGMFlags : int
	{
		STGM_DIRECT = 0,
		STGM_FAILIFTHERE = 0,
		STGM_READ = 0,
		STGM_WRITE = 1,
		STGM_READWRITE = 2,
		STGM_SHARE_EXCLUSIVE = 0x10,
		STGM_SHARE_DENY_WRITE = 0x20,
		STGM_SHARE_DENY_READ = 0x30,
		STGM_SHARE_DENY_NONE = 0x40,
		STGM_CREATE = 0x1000,
		STGM_TRANSACTED = 0x10000,
		STGM_CONVERT = 0x20000,
		STGM_PRIORITY = 0x40000,
		STGM_NOSCRATCH = 0x100000,
		STGM_NOSNAPSHOT = 0x200000,
		STGM_DIRECT_SWMR = 0x400000,
		STGM_DELETEONRELEASE = 0x4000000,
		STGM_SIMPLE = 0x8000000,
	}
	internal enum StatFlag
	{
		STATFLAG_DEFAULT = 0,
		STATFLAG_NONAME = 1,
		STATFLAG_NOOPEN = 2
	}
	internal enum STGTYFlag
	{
		STGTY_STORAGE = 1,
		STGTY_STREAM = 2,
		STGTY_LOCKBYTES = 3,
		STGTY_PROPERTY = 4
	}

	internal enum TCIFlags : int
	{
		TCI_SRCCHARSET = 1,
		TCI_SRCCODEPAGE = 2,
		TCI_SRCFONTSIG = 3
	}
}