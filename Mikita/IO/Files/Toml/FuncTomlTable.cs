using System;
using Tomlyn.Model;

namespace Mikita.IO.Files.Toml;

public sealed class FuncTomlTable
	(
		Func<TomlTable> value
	)
	: ITomlTable
	{
		public TomlTable Value
			=> value();
	}