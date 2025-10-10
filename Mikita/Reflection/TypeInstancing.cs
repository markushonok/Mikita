using Mikita.Nulls;
using System;

namespace Mikita.Reflection;

public static class TypeInstancing
	{
		public static T NewInstance<T>(this Type type)
		{
			if (!typeof(T).IsAssignableFrom(type))
				{
					throw new InvalidOperationException(
						$"{type} is not assignable to {typeof(T)}");
				}

			return (T) type.NewInstance().NotNull();
		}

		public static object NewInstance(this Type type)
			=> Activator.CreateInstance(type).NotNull();
	}