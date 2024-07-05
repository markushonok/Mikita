namespace Mikita.UI.CornerQuads;

public static class DefaultCornerQuad
	{
		public static CornerQuadRecord<int> Major { get; }
			= CornerQuad.WithRadii(16);

		public static CornerQuadRecord<int> Minor { get; }
			= CornerQuad.WithRadii(8);
	}