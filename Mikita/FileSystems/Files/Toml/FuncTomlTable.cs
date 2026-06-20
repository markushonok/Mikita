using System;
using Tomlyn.Model;

namespace Mikita.FileSystems.Files.Toml;

public sealed class FuncTomlTable
	(
		Func<TomlTable> value
	)
	: ITomlTable
	{
		public TomlTable Value
			=> value();
	}