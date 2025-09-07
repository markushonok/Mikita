using Mikita.FileSystems.Files;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.FileSystems.Folders;

public static class EntryEnumeration
	{
		public static IAsyncEnumerable<IFile> Files
			(
				this IFolder folder
			)
			=> folder.Entries
				.Select(x => x.AsFile)
				.Where(x => x.Exists);

		public static IAsyncEnumerable<IFolder> SubFolders
			(
				this IFolder folder
			)
			=> folder.Entries
				.Select(x => x.AsFolder)
				.Where(x => x.Exists);
	}