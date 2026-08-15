using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.IO.Files;

public static class FileReading
	{
		extension(IReadOnlyFile file)
			{
				public Task<string> ReadString
					(
						CancellationToken cancel = default
					)
					=> file.ReadString(Encoding.UTF8, cancel);

				public async Task<string> ReadString
					(
						Encoding encoding,
						CancellationToken cancel = default
					)
					{
						await using var stream = await file.Open(cancel);
						using var reader = new StreamReader(stream, encoding);
						return await reader.ReadToEndAsync(cancel);
					}
			}
	}