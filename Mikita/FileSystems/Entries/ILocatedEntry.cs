using Mikita.FileSystems.Paths;
using System.Collections.Generic;

namespace Mikita.FileSystems.Entries;

public interface ILocatedEntry
	{
		void Create();

		void Delete();

		bool Exists { get; }

		IPath Path { get; }
	}