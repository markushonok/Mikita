using Mikita.FileSystems.Paths;
using System.Linq;
using Tomlyn.Model;

namespace Mikita.FileSystems.Files.Toml;

public sealed class SubTomlTable
	(
		ITomlTable root,
		IPath path
	)
	: ITomlTable
	{
		public TomlTable Value
			=> path.Elements.Aggregate
				(
					root.Value,
					(current, next) => (TomlTable) current[next]
				);
	}