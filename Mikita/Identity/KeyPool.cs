using Mikita.Structs.Referring;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Mikita.Identity;

/// <summary>Provides standard key-pool compositions.</summary>
public static class KeyPool
	{
		/// <summary>
		/// Creates a pool whose first candidate follows <paramref name="lastKey"/>.
		/// </summary>
		public static IKeyPool<T> NewWith<T>
			(
				T lastKey,
				Func<T, T> nextKey,
				Func<T, string> unacquiredKeyMessage
			)
			=> new KeyPool<T>
				(
					occupiedKeys: new HashSet<T>(),
					lastKey: Ref.To(lastKey),
					nextKey,
					unacquiredKeyMessage
				);

		extension<T>(IKeyPool<T> pool)
			{
				/// <summary>
				/// Adds serialized access. The source must no longer be accessed
				/// directly.
				/// </summary>
				public IKeyPool<T> WithSerialAccess
					=> new SerialAccessKeyPool<T>(pool, new Lock());
			}
	}

/// <summary>
/// Issues the next unoccupied key and tracks released keys as available.
/// </summary>
/// <param name="occupiedKeys">Keys unavailable for acquisition.</param>
/// <param name="lastKey">The most recently examined key.</param>
/// <param name="nextKey">Returns the candidate following a key.</param>
/// <param name="unacquiredKeyMessage">Describes an invalid release.</param>
public sealed class KeyPool<T>
	(
		ISet<T> occupiedKeys,
		IRef<T> lastKey,
		Func<T, T> nextKey,
		Func<T, string> unacquiredKeyMessage
	)
	: IKeyPool<T>
	{
		public T Acquire()
			{
				T key;

				do
					{
						key = nextKey(lastKey.Value);
						lastKey.Value = key;
					}
				while (!occupiedKeys.Add(key));

				return key;
			}

		public void Release(T key)
			{
				if (!occupiedKeys.Remove(key))
					throw UnacquiredKeyException(key);
			}

		private Exception UnacquiredKeyException(T key)
			=> new InvalidOperationException(unacquiredKeyMessage(key));
	}