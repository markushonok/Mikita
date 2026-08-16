using Mikita.IO.Files;
using Mikita.IO.Paths;
using Mikita.IO.Paths.Formats;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.IO.FileSystems.Embedded;

public sealed partial class EmbeddedFile
	{
		public static IReadOnlyFile At
			(
				Assembly assembly,
				IPath path
			)
			=> new EmbeddedFile(assembly, path);
	}

public sealed partial class EmbeddedFile
	(
		Assembly assembly,
		IPath path
	)
	: IReadOnlyFile
	{
		public Task<Stream> Open
			(
				CancellationToken cancel = default
			)
			{
				var stream = assembly.GetManifestResourceStream(ResourceName);

				return stream is not null
					? Task.FromResult(stream)
					: Task.FromException<Stream>(NotFoundException);
			}

		public Task<bool> Exists
			(
				CancellationToken cancel = default
			)
			=> Task.FromResult
				(
					assembly.GetManifestResourceInfo(ResourceName) is not null
				);

		public IPath Path
			=> path;

		private string ResourceName
			=> path.With(PathFormat.Unix);

		private Exception NotFoundException
			=> new FileNotFoundException
				(
					$"Embedded resource at '{path.ToDefaultString()}' was not found.",
					path.ToDefaultString()
				);
	}