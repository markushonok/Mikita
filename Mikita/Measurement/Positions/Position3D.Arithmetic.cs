namespace Mikita.Measurement.Positions;

partial interface Position3D<out T>
	{
		public static Position3D<T> operator +
			(
				Position3D<T> augend,
				Position3D<T> addend
			)
			=> new PositionRecord3D<T>
				(
					augend.X + addend.X,
					augend.Y + addend.Y,
					augend.Z + addend.Z
				);
	}