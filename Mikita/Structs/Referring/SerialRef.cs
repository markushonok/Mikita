using System.Threading;

namespace Mikita.Structs.Referring;

/// <summary>
/// Serializes the value access of another <see cref="IRef{T}"/>
/// so reads and writes stay coherent under concurrent use.
/// </summary>
public sealed class SerialRef<T>
	(
		IRef<T> source,
		Lock access
	)
	: IRef<T>
	{
		public T Value
			{
				get
					{
						lock (access) return source.Value;
					}

				set
					{
						lock (access) source.Value = value;
					}
			}
	}