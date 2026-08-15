using Mikita.IO.Files;
using Mikita.IO.Folders;
using System;

namespace Mikita.IO.Entries;

public interface IFoundEntry: IFoundReadOnlyEntry
	{
		void Match
			(
				Action<IFile> file,
				Action<IFolder> folder
			);
	}