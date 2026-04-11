using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Mikita.FileSystems.Files;

public static class FileReading
	{
		extension(IFile file)
			{
				public Task<string> String
					()
					=> file.String(Encoding.UTF8);

				public async Task<string> String
					(
						Encoding encoding
					)
					{
						await using var stream = await file.Open();
						using var reader = new StreamReader(stream, encoding);
						return await reader.ReadToEndAsync();
					}
			}
	}