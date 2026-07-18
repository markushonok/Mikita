using Mikita.Nulls;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Tomlyn;
using Tomlyn.Model;

namespace Mikita.FileSystems.Files.Toml;

public sealed partial class TomlFileIO
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
				var plain = await StringFrom(target, cancel);
				var pretty = PrettyRegex.Replace(plain, "\n\n");
				await file.Write(pretty, cancel);
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

    [GeneratedRegex(@"\n(?=\[)")]
    private static partial Regex PrettyRegex { get; }
}