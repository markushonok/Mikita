namespace Mikita.Structs.Maps;

public interface IReadOnlyMap<in TKey, out T>
	{
		T this[TKey key] { get; }
	}