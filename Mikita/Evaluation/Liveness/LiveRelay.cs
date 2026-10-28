using System;

namespace Mikita.Evaluation.Liveness;

/// <summary>
/// Holds the relay generated for <typeparamref name="T"/>.
/// </summary>
/// <remarks>
/// A source generator assigns <see cref="Factory"/> from a module initializer
/// in the assembly that declares <typeparamref name="T"/>. The assignment
/// happens before any code of that assembly runs, so reads need no
/// synchronization.
/// </remarks>
public static class LiveRelay<T>
	where T: class
	{
		/// <summary>
		/// Creates a relay over a value source, or is <see langword="null"/>
		/// when the declaring project has no manifest entry for
		/// <typeparamref name="T"/>.
		/// </summary>
		public static Func<Func<T>, T>? Factory { get; set; }
	}