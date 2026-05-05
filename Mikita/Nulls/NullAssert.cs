using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

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
		///		<paramref name="expression"/> is null.
		/// </exception>
		[StackTraceHidden, DebuggerHidden]
		public static async Task<T> NotNull<T>
			(
				this Task<T?> value,
				[CallerArgumentExpression("value")] string? expression = null
			)
			{
				if (expression is null) throw NullExpressionException<T>();
				return await value ?? throw NullValueException<T>(expression);
			}

		/// <inheritdoc cref="NotNull{T}(System.Threading.Tasks.Task{T?},string?)" />
		[StackTraceHidden, DebuggerHidden]
		public static T NotNull<T>
			(
				this T? value,
				[CallerArgumentExpression("value")] string? expression = null
			)
			{
				if (expression is null) throw NullExpressionException<T>();
				return value ?? throw NullValueException<T>(expression);
			}

		/// <inheritdoc cref="NotNull{T}(T?,string?)" />
		[StackTraceHidden, DebuggerHidden]
		public static T NotNull<T>
			(
				this T? value,
				[CallerArgumentExpression("value")] string? expression = null
			)
			where T: struct
			{
				if (expression is null) throw NullExpressionException<T>();
				return value ?? throw NullValueException<T>(expression);
			}

		/// <typeparam name="T">The type of the caller.</typeparam>
		///
		/// <returns>
		///		<see cref="ArgumentNullException"/> with a message that
		///		describes that the caller cannot be null.
		/// </returns>
		[StackTraceHidden, DebuggerNonUserCode]
		public static ArgumentNullException NullExpressionException<T>()
			=> new($"{typeof(T).Name} expression cannot be null.");

		/// <typeparam name="T">The type of the value.</typeparam>
		/// 
		/// <returns>
		///		<see cref="ArgumentNullException"/> with a message that describes
		///		that the value cannot be null.
		/// </returns>
		public static NullReferenceException NullValueException<T>
			(
				string expression
			)
			=> new
				(
					$"{typeof(T).Name} `{expression}` asserted"
					+ $" to be non-null but it's null."
				);
	}
