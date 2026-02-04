using Mikita.Measurement.Angles;
using System.Numerics;

namespace Mikita.Measurement.Rotations.EulerAngles;

public static class EulerAngleInstancing
	{
		extension<T>(EulerAngle<T>)
			where T: INumber<T>, IFloatingPointConstants<T>
			{
				public static EulerAngle<T> Zero
					=> EulerAngleInstancing<T>.Zero;
			}
	}

file static class EulerAngleInstancing<T>
	where T: INumber<T>, IFloatingPointConstants<T>
	{
		public static EulerAngle<T> Zero { get; }
			= new
				(
					Angle<T>.Zero,
					Angle<T>.Zero,
					Angle<T>.Zero
				);
	}