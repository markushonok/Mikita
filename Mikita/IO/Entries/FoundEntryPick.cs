using Mikita.IO.Files;
using Mikita.IO.Folders;
using System;

namespace Mikita.IO.Entries;

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

		extension(IFoundReadOnlyEntry entry)
			{
				public IReadOnlyFile? AsFile
					=> entry.Match<IReadOnlyFile?>
						(
							file: x => x,
							folder: y => null
						);

				public IReadOnlyFolder? AsFolder
					=> entry.Match<IReadOnlyFolder?>
						(
							file: x => null,
							folder: y => y
						);

				public T Match<T>
					(
						Func<IReadOnlyFile, T> file,
						Func<IReadOnlyFolder, T> folder
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
