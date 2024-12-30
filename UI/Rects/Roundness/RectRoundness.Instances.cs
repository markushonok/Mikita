using System.Numerics;

namespace Mikita.UI.Rects.Roundness;

public static class RectRoundness
	{
		public static RectRoundnessRecord<T> WithRadii<T>(T radius)
			where T : INumber<T>
			=> new(radius, radius, radius, radius);
		
		public static RectRoundnessRecord<T> WithRadii<T>
			(
				T topLeft, 
				T topRight, 
				T bottomRight,
				T bottomLeft
			)
			where T : INumber<T>
			=> new(topLeft, topRight, bottomRight, bottomLeft);
	}