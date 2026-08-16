using System;
using System.Linq;

namespace Mikita.IO.Paths;

partial class Path
	{
		public override bool Equals
			(
				object? other
			)
			=> other is IPath path
				&& Ascends == path.Ascends
				&& Elements.SequenceEqual(path.Elements);

		public override int GetHashCode()
			{
				var hash = new HashCode();
				hash.Add(Ascends);

				foreach (var element in Elements)
					hash.Add(element, StringComparer.Ordinal);

				return hash.ToHashCode();
			}
	}