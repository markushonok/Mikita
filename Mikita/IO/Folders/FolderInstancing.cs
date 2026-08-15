using Mikita.IO.FileSystems.Native;
using Mikita.IO.Paths;
using System;

namespace Mikita.IO.Folders;

public static class FolderInstancing
	{
		extension(NativeFolder)
			{
				public static IFolder Program
					=> NativeFolder.At
						(
							Path.From(AppDomain.CurrentDomain.BaseDirectory)
						);
			}
	}