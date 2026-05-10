using Mikita.FileSystems.Entries;
using Mikita.FileSystems.Paths;
using Mikita.FileSystems.Paths.Formats;
using Mikita.Structs.Enumerables;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.FileSystems.Folders;

public sealed class Folder
	(
		IPath path
	)
	: IFolder
	{
		public IUnspecifiedEntry EntryAt(IPath subpath)
			=> new Entry(path / subpath);

		public IAsyncEnumerable<IUnspecifiedEntry> Entries
			=> Directory
				.EnumerateFileSystemEntries(PathString)
				.Select(System.IO.Path.GetFileName)
				.WhereNotNull()
				.Select(this.EntryWithName)
				.ToAsyncEnumerable();

		public Task Create
			(
				CancellationToken cancel = default
			)
			=> Task.Run
				(
					() => Directory.CreateDirectory(PathString),
					cancel
				);

		public Task MoveTo
			(
				IPath destination,
				CancellationToken cancel = default
			)
			=> Task.Run
				(
					() => Directory.Move(PathString, destination.ToDefaultString()),
					cancel
				);

		public Task Delete
			(
				CancellationToken cancel = default
			)
			=> Task.Run
				(
					() => Directory.Delete(PathString, recursive: true),
					cancel
				);

		public Task<bool> Exists
			(
				CancellationToken cancel = default
			)
			=> Task.Run
				(
					() => Directory.Exists(PathString),
					cancel
				);

		private string PathString
			=> path.ToDefaultString();
	}