using Mikita.Structs.Referring;
using System;
using Tomlyn.Model;

namespace Mikita.FileSystems.Files.Toml;

public static class TomlTableInstancing
	{
		extension(TomlTable table)
			{
				public ITomlTable AsAbstract
					=> new FuncTomlTable(() => table);
			}

		extension(TomlTable table)
			{
				public static ITomlTable From(IRef<ITomlTable> reference)
					=> TomlTable.From(() => reference.Value.Value);

				public static ITomlTable From(Func<ITomlTable> value)
					=> TomlTable.From(() => value().Value);

				public static ITomlTable From(Func<TomlTable> value)
					=> new FuncTomlTable(value);
			}
	}