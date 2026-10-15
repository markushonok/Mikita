using System.Collections.Generic;

namespace Mikita.Structs.Referring;

/// <summary>
/// Ignores a write whose value equals the current one, so a repeated write
/// reaches neither the source nor any decorator around it.
/// </summary>
public sealed class IgnoringDuplicatesRef<T>
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
						if (EqualityComparer<T>.Default.Equals(source.Value, value)) return;

						source.Value = value;
					}
			}
	}