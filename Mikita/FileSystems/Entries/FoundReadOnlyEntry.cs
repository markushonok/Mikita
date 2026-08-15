using Mikita.FileSystems.Files;
using Mikita.FileSystems.Folders;
using System;

namespace Mikita.FileSystems.Entries;

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