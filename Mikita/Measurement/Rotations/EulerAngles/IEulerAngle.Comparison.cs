using Mikita.Measurement.Angles;

namespace Mikita.Measurement.Rotations.EulerAngles;

public partial interface IEulerAngle<out T>
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