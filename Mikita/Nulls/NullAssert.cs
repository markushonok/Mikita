using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Mikita.Nulls;

public static class NullAssert
	{
		/// <summary>
		///		Asserts that <paramref name="value"/> is not null.
		/// </summary>
		/// 
		/// <returns>Non-null <paramref name="value"/>.</returns>
		///
		/// <exception cref="ArgumentNullException">
		///		Thrown if <paramref name="value"/> or
		///		<paramref name="caller"/> is null.
		/// </exception>
		[StackTraceHidden, DebuggerHidden]
		public static T NotNull<T>
			(
				this T? value,
				[CallerMemberName] string? caller = null
			)
			{
				if (caller is null) throw NullCallerException<T>();
				if (value is null) throw NullValueException<T>(caller);
				return value;
			}

		/// <summary>
		///		Asserts that <paramref name="value"/> is not null.
		/// </summary>
		/// 
		/// <returns>Non-null <paramref name="value"/>.</returns>
		///
		/// <exception cref="ArgumentNullException">
		///		Thrown if <paramref name="value"/> or
		///		<paramref name="caller"/> is null.
		/// </exception>
		[StackTraceHidden, DebuggerHidden]
		public static T NotNull<T>
			(
				this T? value,
				[CallerMemberName] string? caller = null
			)
			where T: struct
			{
				if (caller is null) throw NullCallerException<T>();
				if (value is null) throw NullValueException<T>(caller);
				return value.Value;
			}

		/// <typeparam name="T">The type of the caller.</typeparam>
		///
		/// <returns>
		///		<see cref="ArgumentNullException"/> with a message that
		///		describes that the caller cannot be null.
		/// </returns>
		[StackTraceHidden, DebuggerNonUserCode]
		public static ArgumentNullException NullCallerException<T>()
			=> new(NullCallerMessage<T>());

		/// <typeparam name="T">The type of the caller.</typeparam>
		/// 
		/// <returns>
		///		The string message that describes the <typeparamref name="T"/>
		///		caller cannot be null.
		/// </returns>
		public static string NullCallerMessage<T>()
			=> $"{nameof(T)} caller cannot be null.";

		/// <typeparam name="T">The type of the value.</typeparam>
		/// 
		/// <returns>
		///		<see cref="ArgumentNullException"/> with a message that describes
		///		that the value cannot be null.
		/// </returns>
		public static ArgumentNullException NullValueException<T>
			(
				string caller
			)
			=> new(NullValueMessage<T>(caller));

		/// <param name="caller">The name of the caller.</param>
		/// <typeparam name="T">The type of the caller.</typeparam>
		/// <returns>
		///		The string message that describes the <typeparamref name="T"/>
		///		<paramref name="caller"/> asserted to be non-null but it's null.
		/// </returns>
		public static string NullValueMessage<T>
			(
				string caller
			)
			=> $"{nameof(T)} {caller} asserted to be non-null but it's null.";
	}
