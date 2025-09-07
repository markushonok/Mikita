using Mikita.FileSystems.Files;

namespace Mikita.FileSystems.Folders;

public static class EntryPicking
	{
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