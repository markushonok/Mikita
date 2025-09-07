using Mikita.FileSystems.Entries;
using System.IO;

namespace Mikita.FileSystems.Files;

public interface IFile: ILocatedEntry
	{
		Stream Stream { get; }
	}