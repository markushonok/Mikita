using Mikita.FileSystems.Entries;
using Mikita.FileSystems.Files;
using Mikita.FileSystems.Folders;
using Mikita.FileSystems.Paths;
using Mikita.Godot.FileSystems.Files;
using Mikita.Godot.FileSystems.Folders;

namespace Mikita.Godot.FileSystems.Entries;

public sealed class UserEntry
	(
		IPath path,
		string scheme = "user"
	)
	: IUnspecifiedEntry
	{
		public IFile AsFile
			=> new GodotFile(path, scheme);

		public IFolder AsFolder
			=> new GodotFolder(path, scheme);
	}
