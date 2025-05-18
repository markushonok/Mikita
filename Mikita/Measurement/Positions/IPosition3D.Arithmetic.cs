using Mikita.Measurement.Sizes;

namespace Mikita.Measurement.Positions;

partial interface IPosition3D<out T>
	{
		public static Position3D<T> operator +
			(
				IPosition3D<T> augend,
				IPosition3D<T> addend
			)
			=> new
				(
					augend.X + addend.X,
					augend.Y + addend.Y,
					augend.Z + addend.Z
				);

		public static Position3D<T> operator +
			(
				IPosition3D<T> augend,
				ISize3D<T> addend
			)
			=> new
				(
					augend.X + addend.Width,
					augend.Y + addend.Height,
					augend.Z + addend.Depth
				);

		public static Position3D<T> operator -
			(
				IPosition3D<T> minuend,
				ISize3D<T> subtrahend
			)
			=> Position.At
				(
					minuend.X - subtrahend.Width,
					minuend.Y - subtrahend.Height,
					minuend.Z - subtrahend.Depth
				);
	}