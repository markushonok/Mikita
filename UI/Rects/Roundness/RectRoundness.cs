using System.Numerics;

namespace Mikita.UI.Rects.Roundness;

public interface RectRoundness<out T> 
	where T : INumber<T>
	{
		T TopLeft { get; }

		T TopRight { get; }

		T BottomRight { get; }

		T BottomLeft { get; }
	}

public static class RectRoundness
	{
		public static RectRoundnessRecord<T> WithRadii<T>(T radius)
			where T : INumber<T>
			=> new(radius, radius, radius, radius);
		
		public static RectRoundnessRecord<T> WithRadii<T>(T a, T b, T c, T d)
			where T : INumber<T>
			=> new(a, b, c, d);
	}