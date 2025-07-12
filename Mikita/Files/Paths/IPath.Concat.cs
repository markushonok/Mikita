using System.Linq;

namespace Mikita.Files.Paths;

public partial interface IPath
	{
		static Path operator /
			(
				IPath left,
				IPath right
			)
			=> new(left.Elements.Concat(right.Elements));
	}