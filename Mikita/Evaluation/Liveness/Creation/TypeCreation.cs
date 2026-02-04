using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using static System.Reflection.TypeAttributes;

namespace Mikita.Evaluation.Liveness.Creation;

public static class TypeCreation
	{
		public static Type Create<T>()
			{
				var type = Define<T>();
				type.CreateMembers<T>();
				return type.CreateType();
			}

		public static TypeBuilder Define<T>()
			=> LiveModule.Builder.DefineType
				(
					FullNameFor<T>(),
					Attributes,
					parent: typeof(object),
					interfaces: [typeof(T)]
				);

		public static string FullNameFor<T>()
			=> FullNameFor(typeof(T));
		
		public static string FullNameFor(Type type)
			=> $"{Live.Namespace}.Live_{NameFor(type)}";

		private static string NameFor(Type type)
			{
				if (type.IsGenericType)
					{
						var genericName = type.GetGenericTypeDefinition().Name;
						var tickIndex = genericName.IndexOf('`');
						if (tickIndex >= 0)
							genericName = genericName[..tickIndex];

						var args = type.GetGenericArguments()
							.Select(NameFor);

						return $"{genericName}_{string.Join("_", args)}";
					}

				return type.IsArray
					? $"{NameFor(type.GetElementType()!)}_Array{type.GetArrayRank()}"
					: type.Name.Replace('+', '_');

			}

		public static TypeAttributes Attributes
			=> Public
			 | Class
			 | AutoClass
			 | AnsiClass
			 | Sealed
			 | BeforeFieldInit;
	}