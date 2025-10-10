using Mikita.FileSystems.Files;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.FileSystems.Folders;

public static class FilePicking
	{
		public static IAsyncEnumerable<IFile> Files
			(
				this IFolder folder
			)
			=> folder.Entries
				.Select(x => x.AsFile)
				.Where(x => x.Exists);

		public static bool ContainsFileWithName
			(
				this IFolder folder,
				string name
			)
			=> folder.FileWithName(name).Exists;

		public static IFile FileWithName
			(
				this IFolder folder,
				string name
			)
			=> folder.EntryWithName(name).AsFile;
	}