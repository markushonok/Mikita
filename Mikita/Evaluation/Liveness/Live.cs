using System;

namespace Mikita.Evaluation.Liveness;

public static class Live
	{
		/// <summary>
		/// Represents <paramref name="function"/> through a relay for
		/// <typeparamref name="T"/>.
		/// </summary>
		/// <remarks>
		/// The relay is generated from the <c>live</c> feature of the
		/// implementation manifest in the project that declares
		/// <typeparamref name="T"/>.
		/// </remarks>
		/// <exception cref="NotSupportedException">
		/// The declaring project has no manifest entry for
		/// <typeparamref name="T"/>.
		/// </exception>
		public static T ResultOf<T>(Func<T> function)
			where T: class
			=> LiveRelay<T>.Factory is {} relay
				? relay(function)
				: throw MissingRelay<T>();

		private static NotSupportedException MissingRelay<T>()
			=> new
				(
					$"No live relay is generated for '{typeof(T)}'. "
					+ "Add the type with the 'live' feature to the "
					+ "Implementations.toml of the project that declares it."
				);
	}