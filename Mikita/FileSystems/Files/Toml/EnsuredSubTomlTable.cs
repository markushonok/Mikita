using Mikita.FileSystems.Paths;
using System.Linq;
using Tomlyn.Model;

namespace Mikita.FileSystems.Files.Toml;

public sealed class EnsuredSubTomlTable
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
					NextTable
				);

		private static TomlTable NextTable
			(
				TomlTable current,
				string nextField
			)
			{
				if (!current.ContainsKey(nextField))
					current.Add(nextField, new TomlTable());

				return (TomlTable) current[nextField];
			}
	}