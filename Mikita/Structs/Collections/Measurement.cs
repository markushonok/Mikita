using System.Collections.Generic;

namespace Mikita.Structs.Collections;

public static class Measurement
	{
		public static bool IsNotEmpty
			(
				this IReadOnlyCollection<object> collection
			)
			=> collection.Count != 0;

		public static bool IsEmpty
			(
				this IReadOnlyCollection<object> collection
			)
			=> collection.Count == 0;
	}