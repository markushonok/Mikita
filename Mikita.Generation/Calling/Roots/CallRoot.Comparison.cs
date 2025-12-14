using Microsoft.CodeAnalysis;
using System;

namespace Mikita.Generation.Calling.Roots;

partial class CallRoot: IEquatable<ICallRoot>
	{
		public override bool Equals
			(
				object? @object
			)
			=> ReferenceEquals(this, @object)
				|| @object is ICallRoot other && Equals(other);

		public bool Equals
			(
				ICallRoot? other
			)
			=> other != null
				&& Comparer.Equals(EnclosingMethod, other.EnclosingMethod)
				&& Comparer.Equals(TargetType, other.TargetType);

		public override int GetHashCode()
			{
				unchecked
					{
						var hashCode1 = Comparer.GetHashCode(EnclosingMethod);
						var hashCode2 = Comparer.GetHashCode(TargetType);
						return (hashCode1 * 397) ^ hashCode2;
					}
			}

		private static SymbolEqualityComparer Comparer
			=> SymbolEqualityComparer.Default;
	}