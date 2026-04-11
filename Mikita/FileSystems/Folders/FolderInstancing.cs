using Mikita.FileSystems.Paths;
using System;

namespace Mikita.FileSystems.Folders;
using TPath = Mikita.FileSystems.Paths.Path;

public static class FolderInstancing
	{
		extension(Folder)
			{
				public static Folder Program
					=> new
						(
							TPath.From(AppDomain.CurrentDomain.BaseDirectory)
						);
			}
	}