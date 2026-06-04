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
	: IFileIO<TomlTable>
	{
		public async Task<TomlTable> Load
			(
				CancellationToken cancel = default
			)
			{
				var @string = await file.ReadString(cancel);
				var table = await TomlFrom(@string, cancel);
				return table;
			}

		private static Task<TomlTable> TomlFrom
			(
				string @string,
				CancellationToken cancel
			)
			=> Task.Run
				(
					() => TomlSerializer.Deserialize<TomlTable>(@string).NotNull(),
					cancel
				);

		public async Task Save
			(
				TomlTable target,
				CancellationToken cancel = default
			)
			{
				var @string = await StringFrom(target, cancel);
				await file.Write(@string, cancel);
			}

		private static Task<string> StringFrom
			(
				TomlTable table,
				CancellationToken cancel
			)
			=> Task.Run
				(
					() => TomlSerializer.Serialize(table),
					cancel
				);
	}