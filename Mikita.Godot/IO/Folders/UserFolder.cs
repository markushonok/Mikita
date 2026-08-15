using Mikita.IO.Folders;
using Mikita.IO.Paths;
using Path = Mikita.IO.Paths.Path;

namespace Mikita.Godot.IO.Folders;

public static class UserFolder
	{
		public static IFolder Root
			=> new GodotFolder
				(
					Path.Current,
					scheme: "user"
				);
	}