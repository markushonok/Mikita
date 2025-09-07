using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Mikita.Evaluation.Liveness.Creation;

public delegate T RelayConstructor<T>(Func<T> function);

public static class ConstructorCreation
	{
		public static void CreateConstructorWith<T>
			(
				this TypeBuilder type,
				FieldInfo field
			)
			{
				type
					.DefineConstructor<T>()
					.ImplementWith(field);
			}

		public static ConstructorBuilder DefineConstructor<T>
			(
				this TypeBuilder type
			)
			{
				return type.DefineConstructor
					(
						MethodAttributes.Public,
						CallingConventions.HasThis,
						parameterTypes: [typeof(Func<T>)]
					);
			}
		
		public static void ImplementWith
			(
				this ConstructorBuilder constructor,
				FieldInfo field
			)
			{
				var il = constructor.GetILGenerator();
				il.Emit(OpCodes.Ldarg_0);
				il.Emit(OpCodes.Ldarg_1);
				il.Emit(OpCodes.Stfld, field);
				il.Emit(OpCodes.Ret);
			}
	}