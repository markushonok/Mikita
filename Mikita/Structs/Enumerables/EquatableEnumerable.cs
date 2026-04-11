using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.Structs.Enumerables;

public sealed class EquatableEnumerable<T>
	(
		IEnumerable<T> source
	)
	: IEnumerable<T>
	{
		IEnumerator IEnumerable.GetEnumerator()
			=> GetEnumerator();

		public IEnumerator<T> GetEnumerator()
			=> source.GetEnumerator();

		public override bool Equals
			(
				object? @object
			)
			=> Equals(@object as IEnumerable<T>);

		public bool Equals(IEnumerable<T>? other)
			=> ReferenceEquals(this, other)
				|| ReferenceEquals(source, other)
				|| other is not null
				&& source.SequenceEqual(other);

		public override int GetHashCode()
			{
				var hash = new HashCode();
				source.ForEach(x => hash.Add(x));
				return hash.ToHashCode();
			}
	}