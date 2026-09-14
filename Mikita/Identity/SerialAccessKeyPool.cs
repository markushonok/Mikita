using System.Threading;

namespace Mikita.Identity;

/// <summary>
/// Serializes access to another key pool.
/// </summary>
public sealed class SerialAccessKeyPool<T>
	(
		IKeyPool<T> source,
		Lock access
	)
	: IKeyPool<T>
	{
		public T Acquire()
			{
				lock (access) return source.Acquire();
			}

		public void Release(T key)
			{
				lock (access) source.Release(key);
			}
	}