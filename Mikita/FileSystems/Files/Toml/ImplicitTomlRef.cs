using Mikita.Structs.Referring;

namespace Mikita.FileSystems.Files.Toml;

public sealed class ImplicitTomlRef<T>
	(
		ITomlTable table,
		string property,
		T @default
	)
	: IRef<T>
	where T: notnull
	{
		public T Value
			{
				get => table.Value.TryGetValue(property, out var value)
					? (T) value
					: @default;
				set
					{
						if (Equals(value, @default)) table.Value.Remove(property);
						else table.Value[property] = value;
					}
			}
	}