using Mikita.Structs.Perhaps;

namespace Mikita.Structs.Cursors;

public interface ICursor<T>: IMaybe<T>
	{
		void Add(T item);

		void Remove();
	}