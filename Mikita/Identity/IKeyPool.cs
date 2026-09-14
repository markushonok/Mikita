namespace Mikita.Identity;

/// <summary>Issues keys that remain unavailable until released.</summary>
public interface IKeyPool<T>
	{
		/// <summary>Acquires an available key.</summary>
		T Acquire();

		/// <summary>Makes an acquired key available again.</summary>
		/// <exception cref="System.InvalidOperationException">
		/// The key is not currently acquired.
		/// </exception>
		void Release(T key);
	}