using System.Collections.Generic;
using System.Linq;

namespace Mikita.FileSystems.Folders;

public static class SubFolderPicking
	{
		public static IAsyncEnumerable<IFolder> SubFolders
			(
				this IFolder folder
			)
			=> folder.Entries
				.Select(x => x.AsFolder)
				.Where(x => x.Exists);

		public static bool ContainsSubFolderWithName
			(
				this IFolder folder,
				string name
			)
			=> folder.SubFolderWithName(name).Exists;

		public static IFolder SubFolderWithName
			(
				this IFolder folder,
				string name
			)
			=> folder.EntryWithName(name).AsFolder;
	}