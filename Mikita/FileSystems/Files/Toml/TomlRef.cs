using Mikita.Structs.Referring;

namespace Mikita.FileSystems.Files.Toml;

public sealed class TomlRef<T>
	(
		ITomlTable table,
		string property
	)
	: IRef<T>
	where T: notnull
	{
		public T Value
			{
				get => (T) table.Value[property];
				set => table.Value[property] = value;
			}
	}