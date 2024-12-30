using System;
using System.Numerics;

namespace Mikita.Math.Numbers;

public static partial class Number
	{
		public static T StepTo<T>
			(
				this T from,
				T to,
				T by
			)
			where T : INumber<T>
			{
				if (from < to) return T.Min(from + by, to);
				if (from > to) return T.Max(from + by, to);
				if (from == to) return to;
				throw new ArgumentException($"Cannot step from {from} to {to}");
			}
	}