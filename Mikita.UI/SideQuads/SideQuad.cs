using System.Numerics;

namespace Mikita.UI.SideQuads;

public interface SideQuad<out T> 
	where T : INumber<T>
	{
		T Left { get; }

		T Top { get; }

		T Right { get; }

		T Bottom { get; }
	}

public static class SideQuad
	{
		public static SideQuadRecord<T> WithSides<T>(T offset)
			where T : INumber<T>
			=> new(offset, offset, offset, offset);
		
		public static SideQuadRecord<T> WithSides<T>
			(
				T left,
				T top,
				T right,
				T bottom
			)
			where T : INumber<T>
			=> new(left, top, right, bottom);
	}