using System.Numerics;

namespace Mikita.Graphics.Colors;

public static class Color
	{
		public static ColorRecord<T> With<T>
			(
				T red,
				T green,
				T blue
			)
			where T : INumber<T>
			=> new(red, green, blue, T.One);
		
		public static ColorRecord<T> With<T>
			(
				T red,
				T green,
				T blue,
				T alpha
			)
			=> new(red, green, blue, alpha);
	}