using Mikita.IO.Entries;
using Mikita.IO.Paths;

namespace Mikita.IO.FileSystems.Embedded;

public readonly record struct ChildEntryInfo
	(
		IPath Parent,
		FoundEntryInfo Info
	);