using Mikita.FileSystems.Files;
using Mikita.FileSystems.Folders;
using System;

namespace Mikita.FileSystems.Entries;

public interface IFoundEntry: IFoundReadOnlyEntry
	{
		void Match
			(
				Action<IFile> file,
				Action<IFolder> folder
			);
	}