using Mikita.Nulls;
using System.Threading;
using System.Threading.Tasks;
using Tomlyn;
using Tomlyn.Model;

namespace Mikita.FileSystems.Files.Toml;

public sealed class TomlFileIO
	(
		IFile file
	)
	: IFileIO<ITomlTable>
	{
		public async Task<ITomlTable> Load
			(
				CancellationToken cancel = default
			)
			{
				var @string = await file.ReadString(cancel);
				var table = await TomlFrom(@string, cancel);
				return table;
			}

		private static Task<ITomlTable> TomlFrom
			(
				string @string,
				CancellationToken cancel
			)
			=> Task.Run
				(
					() => TomlSerializer
						.Deserialize<TomlTable>(@string)
						.NotNull()
						.AsAbstract,
					cancel
				);

		public async Task Save
			(
				ITomlTable target,
				CancellationToken cancel = default
			)
			{
				var @string = await StringFrom(target, cancel);
				await file.Write(@string, cancel);
			}

		private static Task<string> StringFrom
			(
				ITomlTable table,
				CancellationToken cancel
			)
			=> Task.Run
				(
					() => TomlSerializer.Serialize(table.Value),
					cancel
				);
	}