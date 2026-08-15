using Mikita.FileSystems.Files;
using Mikita.FileSystems.Folders;
using System;

namespace Mikita.FileSystems.Entries;

public interface IFoundReadOnlyEntry
	{
		void Match
			(
				Action<IReadOnlyFile> file,
				Action<IReadOnlyFolder> folder
			);
	}