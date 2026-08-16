using Mikita.IO.Files;
using Mikita.IO.Folders;
using Mikita.IO.Paths;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.IO.Entries;

public static class FoundReadOnlyEntry
	{
		extension(IReadOnlyFile file)
			{
				public IFoundReadOnlyEntry AsFoundReadOnlyEntry
					=> new FileFoundReadOnlyEntry(file);
			}

		private sealed class FileFoundReadOnlyEntry
			(
				IReadOnlyFile target
			)
			: IFoundReadOnlyEntry
			{
				public void Match
					(
						Action<IReadOnlyFile> file,
						Action<IReadOnlyFolder> folder
					)
					=> file(target);
			}

		extension(IReadOnlyFolder folder)
			{
				public IAsyncEnumerable<IFoundReadOnlyEntry> EntriesWith
					(
						IEnumerable<FoundEntryInfo> infos
					)
					=> infos
						.Select(folder.EntryWith)
						.ToAsyncEnumerable();

				private IFoundReadOnlyEntry EntryWith
					(
						FoundEntryInfo info
					)
					{
						var path = SingleElementPath.From(info.Name);

						return info.Type switch
							{
								EntryType.File => folder.FileAt(path).AsFoundReadOnlyEntry,
								EntryType.Folder => folder.SubFolderAt(path).AsFoundReadOnlyEntry,
								_ => throw new NotSupportedException()
							};
					}

				public IFoundReadOnlyEntry AsFoundReadOnlyEntry
					=> new FolderFoundReadOnlyEntry(folder);
			}

		private sealed class FolderFoundReadOnlyEntry
			(
				IReadOnlyFolder target
			)
			: IFoundReadOnlyEntry
			{
				public void Match
					(
						Action<IReadOnlyFile> file,
						Action<IReadOnlyFolder> folder
					)
					=> folder(target);
			}

	}