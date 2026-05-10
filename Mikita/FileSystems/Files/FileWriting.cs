using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.FileSystems.Files;

public static class FileWriting
	{
		extension(IFile file)
			{
				public Task Write
					(
						string @string,
						CancellationToken cancel = default
					)
					=> file.Write
						(
							@string,
							Encoding.UTF8,
							cancel
						);

				public async Task Write
					(
						string @string,
						Encoding encoding,
						CancellationToken cancel = default
					)
					{
						await using var stream = await file.Open(cancel: cancel);
						await using var reader = new StreamWriter(stream, encoding);
						await reader.WriteAsync(@string);
					}
			}
	}