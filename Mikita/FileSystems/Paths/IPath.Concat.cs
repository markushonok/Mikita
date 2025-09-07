using System.Linq;

namespace Mikita.FileSystems.Paths;

public partial interface IPath
	{
		public static Path operator /
			(
				IPath left,
				IPath right
			)
			=> right.Ascends >= left.Elements.Count
				? right.BaseAscendedBy(left.Ascends - left.Elements.Count)
				: left.TrimThenAppend(right);


		public static Path operator /
			(
				IPath left,
				string right
			)
			=> new
				(
					left.Elements.Append(right).ToArray(),
					left.Ascends
				);
	}