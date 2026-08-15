using Mikita.IO.Files;
using Mikita.IO.Folders;
using Mikita.IO.Paths;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.IO.Entries;

public static class FoundEntry
	{
		extension(IFile file)
			{
				public IFoundEntry AsFoundEntry
					=> new FileFoundEntry(file);
			}

		private sealed class FileFoundEntry
			(
				IFile target
			)
			: IFoundEntry
			{
				public void Match
					(
						Action<IReadOnlyFile> file,
						Action<IReadOnlyFolder> folder
					)
					=> file(target);

				public void Match
					(
						Action<IFile> file,
						Action<IFolder> folder
					)
					=> file(target);
			}

		extension(IFolder folder)
			{
				public IAsyncEnumerable<IFoundEntry> EntriesWith
					(
						IEnumerable<FoundEntryInfo> infos
					)
					=> infos
						.Select(folder.EntryWith)
						.ToAsyncEnumerable();

				private IFoundEntry EntryWith
					(
						FoundEntryInfo info
					)
					{
						var path = SingleElementPath.From(info.Name);

						return info.Type switch
							{
								EntryType.File => folder.FileAt(path).AsFoundEntry,
								EntryType.Folder => folder.SubFolderAt(path).AsFoundEntry,
								_ => throw new NotSupportedException()
							};
					}

				public IFoundEntry AsFoundEntry
					=> new FolderFoundEntry(folder);
			}

		private sealed class FolderFoundEntry
			(
				IFolder target
			)
			: IFoundEntry
			{
				public void Match
					(
						Action<IReadOnlyFile> file,
						Action<IReadOnlyFolder> folder
					)
					=> folder(target);

				public void Match
					(
						Action<IFile> file,
						Action<IFolder> folder
					)
					=> folder(target);
			}
	}
