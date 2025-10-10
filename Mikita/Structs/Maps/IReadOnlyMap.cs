namespace Mikita.Structs.Maps;

public interface IReadOnlyMap<in TKey, out T>
	{
		T this[TKey key] { get; }

		bool Contains(TKey key);
	}

public interface IMap<in TKey, T>: IReadOnlyMap<TKey, T>
	{
		new T this[TKey key] { get; set; }

		void Remove(TKey key);
	}