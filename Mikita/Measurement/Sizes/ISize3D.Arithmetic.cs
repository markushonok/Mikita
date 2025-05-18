namespace Mikita.Measurement.Sizes;

public partial interface ISize3D<out T>
	{
		static ISize3D<T> operator +
			(
				ISize3D<T> augend,
				ISize3D<T> addend
			)
			=> Size.Of
				(
					augend.Width + addend.Width,
					augend.Height + addend.Height,
					augend.Depth + addend.Depth
				);

		static ISize3D<T> operator -
			(
				ISize3D<T> minuend,
				ISize3D<T> subtrahend
			)
			=> Size.Of
				(
					minuend.Width - subtrahend.Width,
					minuend.Height - subtrahend.Height,
					minuend.Depth - subtrahend.Depth
				);

		static ISize3D<T> operator *
			(
				ISize3D<T> multiplicand,
				T multiplier
			)
			=> Size.Of
				(
					multiplicand.Width * multiplier,
					multiplicand.Height * multiplier,
					multiplicand.Depth * multiplier
				);

		static ISize3D<T> operator /
			(
				ISize3D<T> dividend,
				T divisor
			)
			=> Size.Of
				(
					dividend.Width / divisor,
					dividend.Height / divisor,
					dividend.Depth / divisor
				);
	}