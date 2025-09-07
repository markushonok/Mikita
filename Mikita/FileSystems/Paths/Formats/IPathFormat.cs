namespace Mikita.FileSystems.Paths.Formats;

public interface IPathFormat
	{
		void ApplyTo
			(
				IPath path,
				out string formatted
			);

		void Parse
			(
				string formatted,
				out IPath path
			);

		bool IsSupport(string formatted);
	}