using Godot;
using Mikita.FileSystems.Files;
using Mikita.FileSystems.Paths;
using Mikita.FileSystems.Paths.Formats;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Godot.FileSystems.Files;

public sealed class GodotFile
	(
		IPath path,
		string scheme
	)
	: IFile
	{
		public async Task<Stream> Open
			(
				FileMode mode = FileMode.Open,
				System.IO.FileAccess access = System.IO.FileAccess.ReadWrite,
				FileShare share = FileShare.Read,
				CancellationToken cancel = default
			)
			=> await Task.Run
				(
					() => new FileStream
						(
							GlobalPathString,
							mode,
							access,
							share,
							bufferSize: 4096,
							options: FileOptions.Asynchronous
						),
					cancel
				);

		public Task Create
			(
				CancellationToken cancel = default
			)
			=> Task.Run
				(
					() => global::Godot.FileAccess.Open(SchemePathString, global::Godot.FileAccess.ModeFlags.Write)?.Dispose(),
					cancel
				);

		public Task MoveTo
			(
				IPath destination,
				CancellationToken cancel = default
			)
			=> Task.Run
				(
					() => DirAccess.RenameAbsolute(SchemePathString, ToSchemePath(destination, scheme)),
					cancel
				);

		public Task Delete
			(
				CancellationToken cancel = default
			)
			=> Task.Run
				(
					() => DirAccess.RemoveAbsolute(SchemePathString),
					cancel
				);

		public Task<bool> Exists
			(
				CancellationToken cancel = default
			)
			=> Task.Run
				(
					() => global::Godot.FileAccess.FileExists(SchemePathString),
					cancel
				);

		public IPath Path
			=> path;

		private string GlobalPathString
			=> ProjectSettings.GlobalizePath(SchemePathString);

		private string SchemePathString
			=> ToSchemePath(path, scheme);

		private static string ToSchemePath
			(
				IPath path,
				string scheme
			)
			{
				var relative = path.ToDefaultString().TrimStart('/');
				var normalizedScheme = scheme.EndsWith("://")
					? scheme
					: $"{scheme}://";

				return relative is ""
					? normalizedScheme
					: $"{normalizedScheme}{relative}";
			}
	}
