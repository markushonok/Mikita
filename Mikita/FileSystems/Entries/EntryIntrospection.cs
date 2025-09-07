namespace Mikita.FileSystems.Entries;

public static class EntryIntrospection
	{
		public static bool IsFile
			(
				this IUnspecifiedEntry entry
			)
			=> entry.AsFile.Exists;

		public static bool IsFolder
			(
				this IUnspecifiedEntry entry
			)
			=> entry.AsFolder.Exists;
	}