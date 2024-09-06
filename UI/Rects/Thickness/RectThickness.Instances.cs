using System.Numerics;

namespace Mikita.UI.Rects.Thickness;

public partial interface RectThickness<out T>
	{
		static RectThicknessRecord<T> No
			=> RectThickness.Of(T.Zero);
	}

public static class RectThickness
	{
		public static RectThicknessRecord<T> Of<T>(T width)
			where T : INumber<T>
			=> new(width, width, width, width);
		
		public static RectThicknessRecord<T> Of<T>(T width, T height)
			where T : INumber<T>
			=> new(width, height, width, height);
		
		public static RectThicknessRecord<T> Of<T>
			(
				T left,
				T top,
				T right,
				T bottom
			)
			where T : INumber<T>
			=> new(left, top, right, bottom);
	}