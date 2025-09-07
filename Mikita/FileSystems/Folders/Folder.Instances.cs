using System;
using TPath = Mikita.FileSystems.Paths.Path;

namespace Mikita.FileSystems.Folders;

partial class Folder
	{
		public static Folder Program
			=> new(TPath.From(AppDomain.CurrentDomain.BaseDirectory));
	}