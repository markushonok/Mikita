using Mikita.IO.Paths;
using Mikita.Structs.Referring;

namespace Mikita.IO.Files.Toml;

public sealed class TomlRef<T>
	(
		ITomlTable root,
		IPath path
	)
	: IRef<T>
	where T: notnull
	{
		public T Value
			{
				get => (T) SubTable.Value[Field];
				set => SubTable.Value[Field] = value;
			}

		private ITomlTable SubTable
			=> root.SubTableAt(path.HigherBy(1));

		private string Field
			=> path.Elements[^1];
	}