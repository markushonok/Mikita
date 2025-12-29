using Mikita.Structs.Perhaps;

namespace Mikita.Structs.Cursors;

public static class CursorInsurance
	{
		extension<T>(ICursor<T> cursor)
			{
				public void Ensure(T compensation)
					{
						if (cursor.IsNone)
							cursor.Add(compensation);
					}
			}
	}