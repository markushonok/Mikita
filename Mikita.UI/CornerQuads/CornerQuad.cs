using System.Numerics;

namespace Mikita.UI.CornerQuads;

public interface CornerQuad<out T> 
	where T : INumber<T>
	{
		T A { get; }

		T B { get; }

		T C { get; }

		T D { get; }
	}

public static class CornerQuad
	{
		public static CornerQuadRecord<T> WithRadii<T>(T radius)
			where T : INumber<T>
			=> new(radius, radius, radius, radius);
		
		public static CornerQuadRecord<T> WithRadii<T>(T a, T b, T c, T d)
			where T : INumber<T>
			=> new(a, b, c, d);
	}