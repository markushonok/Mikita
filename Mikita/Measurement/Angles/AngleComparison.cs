using System.Numerics;

namespace Mikita.Measurement.Angles;

public static class AngleComparison
	{
		extension<T>(IAngle<T>)
			where T: 
				INumber<T>, 
				IFloatingPointConstants<T>
			{
				public static bool operator <
					(
						IAngle<T> comparandum,
						IAngle<T> comparatum
					)
					=> comparandum.InRadians < comparatum.InRadians;

				public static bool operator >
					(
						IAngle<T> comparandum,
						IAngle<T> comparatum
					)
					=> comparandum.InRadians > comparatum.InRadians;
			}
	}