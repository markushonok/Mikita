namespace Mikita.Structs.Cursors;

public interface IBlindCursor<in T>
	{
		void Add(T item);

		void Remove();
	}