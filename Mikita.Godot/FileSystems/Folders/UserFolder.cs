using Mikita.FileSystems.Folders;
using Mikita.FileSystems.Paths;
using Path = Mikita.FileSystems.Paths.Path;

namespace Mikita.Godot.FileSystems.Folders;

public static class UserFolder
	{
		public static IFolder Root
			=> new GodotFolder
				(
					Path.Current,
					scheme: "user"
				);
	}