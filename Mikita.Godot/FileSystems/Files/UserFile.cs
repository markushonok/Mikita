using Mikita.FileSystems.Files;
using Mikita.FileSystems.Paths;

namespace Mikita.Godot.FileSystems.Files;

public static class UserFile
	{
		public static IFile At(IPath path)
			=> new GodotFile
				(
					path,
					scheme: "user"
				);
	}