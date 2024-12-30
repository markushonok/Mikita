using System;

namespace Mikita.Structs.Arrays;

public static class Array
	{
		public static T[] FilledWith<T>(int length, T filler)
			{
				var array = new T[length];
				array.FillWith(filler);
				return array;
			}
		
		public static T[] FilledWith<T>(int length, Func<T> filler)
			{
				var array = new T[length];
				array.FillWith(filler);
				return array;
			}
		
		public static void FillWith<T>(this T[] array, T filler)
			{
				for (var i = 0; i < array.Length; i++) 
					array[i] = filler;
			}

		public static void FillWith<T>(this T[] array, Func<T> filler)
			{
				for (var i = 0; i < array.Length; i++) 
					array[i] = filler();
			}
	}