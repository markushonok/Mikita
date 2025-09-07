using System;
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
			{
				return LiveModule.Builder.DefineType
					(
						MakeUpFullNameFor<T>(),
						Attributes,
						parent: typeof(object),
						interfaces: [typeof(T)]
					);
			}

		public static string MakeUpFullNameFor<T>()
			=> MakeUpFullNameFor(typeof(T));
		
		public static string MakeUpFullNameFor(Type type)
			=> $"{Live.Namespace}.Relay_{type.Name}";

		public static TypeAttributes Attributes
			=> Public
			 | Class
			 | AutoClass
			 | AnsiClass
			 | Sealed
			 | BeforeFieldInit;
	}