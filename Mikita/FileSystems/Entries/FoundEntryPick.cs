using Mikita.FileSystems.Files;
using Mikita.FileSystems.Folders;
using System;

namespace Mikita.FileSystems.Entries;

public static class FoundEntryPick
	{
		extension(IFoundEntry entry)
			{
				public IFile? AsFile
					=> entry.Match<IFile?>
						(
							file: x => x,
							folder: y => null
						);

				public IFolder? AsFolder
					=> entry.Match<IFolder?>
						(
							file: x => null,
							folder: y => y
						);

				public T Match<T>
					(
						Func<IFile, T> file,
						Func<IFolder, T> folder
					)
					{
						var result = default(T);

						entry.Match
							(
								x => result = file(x),
								x => result = folder(x)
							);

						return result!;
					}
			}
	}
