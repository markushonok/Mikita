using Mikita.Measurement.Angles;

namespace Mikita.Measurement.Rotations;

public partial interface Rotation2D<out T>
	{
		bool IsNotRotated
			=> !IsRotated;
		
		bool IsRotated
			=> IsHorizontalRotated || IsVerticalRotated;

		bool IsNotHorizontalRotated
			=> !IsHorizontalRotated;
		
		bool IsHorizontalRotated
			=> Horizontal != Angle<T>.Zero;

		bool IsNotVerticalRotated
			=> !IsVerticalRotated;
		
		bool IsVerticalRotated
			=> Vertical != Angle<T>.Zero;
	}