using Mikita.FileSystems.Paths;
using Mikita.Structs.Referring;
using Tomlyn.Model;

namespace Mikita.FileSystems.Files.Toml;

public static class TomlPathAccess
	{
		extension(ITomlTable root)
			{
				public IRef<T> RefTo<T>(IPath path, T @default)
					where T: notnull
					{
						var table = root.SubTableAt(path.HigherBy(1));
						var property = path.Elements[^1];
						return new ImplicitTomlRef<T>(table, property, @default);
					}

				public IRef<T> RefTo<T>(IPath path)
					where T: notnull
					=> new TomlRef<T>(root, path);

				public TomlTable TableAt(IPath path)
					{
						var table = root.SubTableAt(path.HigherBy(1));
						var property = path.Elements[^1];
						return (TomlTable) table.Value[property];
					}

				public T ValueAt<T>(IPath path)
					{
						var table = root.SubTableAt(path.HigherBy(1));
						var property = path.Elements[^1];
						return (T) table.Value[property];
					}

				public void SetValueAt<T>(IPath path, T value)
					where T: notnull
					{
						var table = root.EnsuredSubTableAt(path.HigherBy(1));
						var field = path.Elements[^1];
						table.Value.Add(field, value);
					}

				public ITomlTable EnsuredSubTableAt(IPath path)
					=> new EnsuredSubTomlTable(root, path);

				public ITomlTable SubTableAt(IPath path)
					=> new SubTomlTable(root, path);
			}
	}