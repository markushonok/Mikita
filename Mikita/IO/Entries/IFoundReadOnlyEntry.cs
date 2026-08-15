using Mikita.IO.Files;
using Mikita.IO.Folders;
using System;

namespace Mikita.IO.Entries;

public interface IFoundReadOnlyEntry
	{
		void Match
			(
				Action<IReadOnlyFile> file,
				Action<IReadOnlyFolder> folder
			);
	}