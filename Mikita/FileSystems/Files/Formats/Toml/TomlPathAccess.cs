using Mikita.FileSystems.Paths;
using Mikita.Structs.Referring;
using System.Linq;
using Tomlyn.Model;

namespace Mikita.FileSystems.Files.Formats.Toml;

public static class TomlPathAccess
	{
		extension(TomlTable root)
			{
				public IRef<T> RefTo<T>(IPath path)
					{
						var table = root.SubTableAt(path.HigherBy(1));
						var property = path.Elements.Last();
						return new TomlRef<T>(table, property);
					}

				public TomlTable SubTableAt(IPath path)
					=> path.Elements.Aggregate
						(
							root,
							(current, next) => (TomlTable) current[next]
						);
			}
	}