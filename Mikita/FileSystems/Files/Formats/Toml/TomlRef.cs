using Mikita.Structs.Referring;
using Tomlyn.Model;

namespace Mikita.FileSystems.Files.Formats.Toml;

public sealed class TomlRef<T>
	(
		TomlTable table,
		string property
	)
	: IRef<T>
	{
		public T Value
			{
				get => (T) table[property];
				set => table[property] = value!;
			}
	}