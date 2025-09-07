using System.Collections.Generic;

namespace Mikita.Structs.Collections;

public static class Containing
	{
		public static bool NotIn
			(
				this object value,
				params ICollection<object> collection
			)
			=> !value.In(collection);

		public static bool In
			(
				this object value,
				params ICollection<object> collection
			)
			=> collection.Contains(value);
	}