using Mikita.FileSystems.Entries;
using Mikita.FileSystems.Paths;
using Mikita.FileSystems.Paths.Formats;
using Mikita.Structs.Enumerables;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mikita.FileSystems.Folders;

public sealed partial class Folder
	(
		IPath path
	)
	: IFolder
	{
		public IUnspecifiedEntry EntryWithName(string name)
			=> new Entry(path / name);

		public IAsyncEnumerable<IUnspecifiedEntry> Entries
			=> Directory
				.EnumerateFileSystemEntries(PathString)
				.Select(System.IO.Path.GetFileName)
				.WhereNotNull()
				.Select(EntryWithName)
				.ToAsyncEnumerable();

		public void Create()
			=> Directory.CreateDirectory(PathString);

		public void Delete()
			=> Directory.Delete(PathString, recursive: true);

		public bool Exists
			=> Directory.Exists(PathString);

		public IPath Path
			=> path;

		private string PathString
			=> path.ToDefaultString();
	}