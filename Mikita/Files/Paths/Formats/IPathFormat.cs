namespace Mikita.Files.Paths.Formats;

public interface IPathFormat
	{
		void Apply
			(
				IPath source,
				out string formatted
			);
	}