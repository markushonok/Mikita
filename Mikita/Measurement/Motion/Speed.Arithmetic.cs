using Mikita.Measurement.Lengths;
using System;

namespace Mikita.Measurement.Motion;

partial interface Speed<out T>
	{
		static Speed<T> operator +
			(
				Speed<T> augend,
				Speed<T> addend
			)
			=> Speed.FromMetersPerSecond
				(
					augend.InMetersPerSecond 
					+ addend.InMetersPerSecond
				);
		
		static Speed<T> operator -
			(
				Speed<T> minuend,
				Speed<T> subtrahend
			)
			=> Speed.FromMetersPerSecond
				(
					minuend.InMetersPerSecond 
					- subtrahend.InMetersPerSecond
				);
		
		static Speed<T> operator *
			(
				Speed<T> multiplicand,
				T multiplier
			)
			=> Speed.FromMetersPerSecond
				(
					multiplicand.InMetersPerSecond 
					* multiplier
				);
		
		static Speed<T> operator /
			(
				Speed<T> dividend,
				T divisor
			)
			=> Speed.FromMetersPerSecond
				(
					dividend.InMetersPerSecond 
					/ divisor
				);

		static Length<T> operator *
			(
				Speed<T> speed,
				TimeSpan time
			)
			=> Length.FromMeters
				(
					speed.InMetersPerSecond
						* T.CreateChecked(time.TotalSeconds)
				);
	}