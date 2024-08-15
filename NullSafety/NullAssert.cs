using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Mikita.NullSafety;

public static class NullAssert
	{
		/// <summary>
		/// Asserts that <paramref name="object"/> is not null.
		/// </summary>
		/// 
		/// <returns>Non-null <paramref name="object"/>.</returns>
		///
		/// <exception cref="ArgumentNullException">
		/// Thrown if <paramref name="caller"/> is null.
		/// </exception>
		/// 
		/// <exception cref="NullReferenceException">
		/// Thrown if <paramref name="object"/> is null.
		/// </exception>
		[StackTraceHidden, DebuggerNonUserCode]
		public static T NotNull<T>
			(
				this T? @object, 
				[CallerMemberName] string? caller = null
			) 
			where T : class
			{
				if (caller is null)
					throw new ArgumentNullException(NullCallerMessage<T>());
				if (@object is null) 
					throw new NullReferenceException(NullValueMessage<T>(caller));
				return @object;
			}

		/// <summary>
		/// Asserts that <paramref name="object"/> is not null.
		/// </summary>
		/// 
		/// <returns>Non-null <paramref name="object"/>.</returns>
		///
		/// <exception cref="ArgumentNullException">
		/// Thrown if <paramref name="caller"/> is null.
		/// </exception>
		/// 
		/// <exception cref="NullReferenceException">
		/// Thrown if <paramref name="object"/> is null.
		/// </exception>
		[StackTraceHidden, DebuggerNonUserCode]
		public static T NotNull<T>
			(
				this T? @object, 
				[CallerMemberName] string? caller = null
			) 
			where T : struct
			{
				if (caller is null)
					throw new ArgumentNullException(NullCallerMessage<T>());
        if (@object is null) 
					throw new NullReferenceException(NullValueMessage<T>(caller));
				return @object.Value;
			}
		
		private static string NullCallerMessage<T>() 
			=> $"{nameof(T)} caller cannot be null.";

		private static string NullValueMessage<T>(string caller) 
			=> $"{nameof(T)} {caller} asserted to be non-null but it's null.";
	}