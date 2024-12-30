using System.Numerics;

namespace Mikita.Measurement.Motion;

public partial interface Speed<out T>
	{
		public static Speed<T> operator +
			(
				Speed<T> augend,
				Speed<T> addend
			)
			=> Speed.InMetersPerSecond
				(
					augend.InMetersPerSecond 
					+ addend.InMetersPerSecond
				);
		
		public static Speed<T> operator -
			(
				Speed<T> minuend,
				Speed<T> subtrahend
			)
			=> Speed.InMetersPerSecond
				(
					minuend.InMetersPerSecond 
					- subtrahend.InMetersPerSecond
				);
		
		public static Speed<T> operator *
			(
				Speed<T> multiplicand,
				T multiplier
			)
			=> Speed.InMetersPerSecond
				(
					multiplicand.InMetersPerSecond 
					* multiplier
				);
		
		public static Speed<T> operator *
			(
				Speed<T> multiplicand,
				Speed<T> multiplier
			)
			=> Speed.InMetersPerSecond
				(
					multiplicand.InMetersPerSecond 
					* multiplier.InMetersPerSecond
				);
		
		public static Speed<T> operator /
			(
				Speed<T> dividend,
				T divisor
			)
			=> Speed.InMetersPerSecond
				(
					dividend.InMetersPerSecond 
					/ divisor
				);
		
		public static Speed<T> operator /
			(
				Speed<T> dividend,
				Speed<T> divisor
			)
			=> Speed.InMetersPerSecond
				(
					dividend.InMetersPerSecond 
					/ divisor.InMetersPerSecond
				);
	}
	
public static partial class Speed
	{
		public static Speed<T> Sqrt<T>
			(
				Speed<T> speed
			)
		
			where T: 
				INumber<T>, 
				IRootFunctions<T>
		
			=> Speed.RootN(speed, 2);
		
		public static Speed<T> RootN<T>
			(
				Speed<T> speed, 
				int n
			)
		
			where T: 
				INumber<T>, 
				IRootFunctions<T>
		
			=> Speed.InMetersPerSecond
				(
					T.RootN(speed.InMetersPerSecond, n)
				);
	}