using Mikita.Structs.Referring;
using System;
using System.Numerics;

namespace Mikita.Math.Numbers;

public static partial class Number
	{
		public static void Step<T>
			(
				this IRef<T> reference,
				T to,
				T by
			)
			where T : INumber<T>
			=> reference.Value = reference.Value.Stepped(to, by);

		public static T Stepped<T>
			(
				this T from,
				T to,
				T by
			)
			where T : INumber<T>
			{
				if (from < to) return T.Min(from + by, to);
				if (from > to) return T.Max(from + by, to);
				return from == to
					? to
					: throw new ArgumentException($"Cannot step from {from} to {to}");
			}
	}