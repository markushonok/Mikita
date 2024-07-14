using System.Diagnostics;

namespace Mikita.NullSafety;

public static class NullAssert
	{
		/// <summary>
		/// Asserts that <paramref name="object"/> is not null.
		/// </summary>
		/// 
		/// <returns>Non-null <paramref name="object"/>.</returns>
		/// 
		/// <exception cref="NullReferenceException">
		/// Thrown if <paramref name="object"/> is null.
		/// </exception>
		[StackTraceHidden]
		public static T NotNull<T>(this T? @object) 
			where T : class
			{
				if (@object is null) 
					throw new NullReferenceException(Message<T>());
				return @object;
			}

		/// <summary>
		/// Asserts that <paramref name="object"/> is not null.
		/// </summary>
		/// 
		/// <returns>Non-null <paramref name="object"/>.</returns>
		/// 
		/// <exception cref="NullReferenceException">
		/// Thrown if <paramref name="object"/> is null.
		/// </exception>
		[StackTraceHidden]
		public static T NotNull<T>(this T? @object) 
			where T : struct
			{
				if (@object is null) 
					throw new NullReferenceException(Message<T>());
				return @object.Value;
			}

		private static string Message<T>() 
			=> $"{nameof(T)} asserted to be non-null but it's null.";
	}