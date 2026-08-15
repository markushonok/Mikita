using System.IO.Enumeration;

namespace Mikita.IO.Entries;

public readonly partial record struct FoundEntryInfo
	{
		public static FoundEntryInfo From
			(
				ref FileSystemEntry source
			)
			=> new
				(
					source.FileName.ToString(),
					source.IsDirectory ? EntryType.Folder : EntryType.File
				);
	}

public readonly partial record struct FoundEntryInfo
	(
		string Name,
		EntryType Type
	);