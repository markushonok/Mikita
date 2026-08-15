using Mikita.IO.Files;
using Mikita.IO.Paths;

namespace Mikita.Godot.IO.Files;

public static class UserFile
	{
		public static IFile At(IPath path)
			=> new GodotFile
				(
					path,
					scheme: "user"
				);
	}