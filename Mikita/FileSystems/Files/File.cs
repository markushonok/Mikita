using Mikita.FileSystems.Paths;
using Mikita.FileSystems.Paths.Formats;
using System.IO;
using SystemFile = System.IO.File;

namespace Mikita.FileSystems.Files;

public sealed partial class File
	(
		IPath path
	)
	: IFile
	{
		public Stream Stream
			=> SystemFile.Open(PathString, FileMode.Open);

		public void Create()
			=> SystemFile.Create(PathString).Dispose();

		public void Delete()
			=> SystemFile.Delete(PathString);

		public bool Exists
			=> SystemFile.Exists(PathString);

		public IPath Path
			=> path;

		private string PathString
			=> path.ToDefaultString();
	}