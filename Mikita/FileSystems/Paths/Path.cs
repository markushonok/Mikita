using Mikita.FileSystems.Paths.Formats;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.FileSystems.Paths;

public sealed class Path
	(
		IReadOnlyCollection<string> elements,
		int ascends
	)
	: IPath
	{
		public override string ToString()
			=> this.ToDefaultString();

		public IReadOnlyCollection<string> Elements
			=> elements;

		public int Ascends
			=> ascends;

		public override bool Equals
			(
				object? @object
			)
			=> Equals(@object as IPath);

		private bool Equals(IPath? other)
			=> other is not null
				&& Elements.SequenceEqual(other.Elements)
				&& Ascends == other.Ascends;

		public override int GetHashCode()
			=> elements
				.Select(x => x.GetHashCode())
				.Append(ascends.GetHashCode())
				.Sum();
	}