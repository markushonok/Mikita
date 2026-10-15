using System;
using System.Collections.Generic;

namespace Mikita.Structs.Referring;

/// <summary>
/// Prohibits a write whose value equals the current one: a repeated write is
/// an invariant violation and throws.
/// </summary>
public sealed class ProhibitingDuplicatesRef<T>
	(
		IRef<T> source
	)
	: IRef<T>
	{
		public T Value
			{
				get => source.Value;

				set
					{
						if (EqualityComparer<T>.Default.Equals(source.Value, value))
							throw DuplicateValueException(value);

						source.Value = value;
					}
			}

		private static Exception DuplicateValueException(T value)
			=> new InvalidOperationException($"The value '{value}' is already held.");
	}