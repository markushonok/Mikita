using Mikita.FileSystems.Paths;
using Mikita.Structs.Referring;
using System.Linq;
using Tomlyn.Model;

namespace Mikita.FileSystems.Files.Toml;

public static class TomlPathAccess
	{
		extension(TomlTable root)
			{
				public IRef<T> RefTo<T>(IPath path)
					{
						var table = root.SubTableAt(path.HigherBy(1));
						var property = path.Elements[^1];
						return new TomlRef<T>(table, property);
					}

				public TomlTable TableAt(IPath path)
					{
						var table = root.SubTableAt(path.HigherBy(1));
						var property = path.Elements[^1];
						return (TomlTable) table[property];
					}

				public T ValueAt<T>(IPath path)
					{
						var table = root.SubTableAt(path.HigherBy(1));
						var property = path.Elements[^1];
						return (T) table[property];
					}

				public TomlTable SubTableAt(IPath path)
					=> path.Elements.Aggregate
						(
							root,
							(current, next) => (TomlTable) current[next]
						);
			}
	}