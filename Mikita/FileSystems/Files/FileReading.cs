using System.IO;
using System.Text;

namespace Mikita.FileSystems.Files;

public static class FileReading
	{
		public static string String
			(
				this IFile file
			)
			=> file.String(Encoding.UTF8);

		public static string String
			(
				this IFile file,
				Encoding encoding
			)
			{
				using var stream = file.Stream;
				using var reader = new StreamReader(stream, encoding);
				return reader.ReadToEnd();
			}
	}