namespace Mikita.Files.Paths.Formats;

public sealed partial class PathFormat
	(
		string separator
	)
	: IPathFormat
	{
		public void Apply
			(
				IPath source,
				out string formatted
			)
			=> formatted = string.Join(separator, source.Elements);
	}