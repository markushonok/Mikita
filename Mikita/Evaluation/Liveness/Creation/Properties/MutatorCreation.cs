using Mikita.Nulls;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Mikita.Evaluation.Liveness.Creation.Properties;

public static class MutatorCreation
	{
		extension(TypeBuilder type)
			{
				public void CreateMutatorFor
					(
						PropertyBuilder property,
						FieldInfo field
					)
					{
						var method = type.DefineMutatorFor(property);
						method.ImplementMutatorWith(field);
						property.SetSetMethod(method);
					}

				public MethodBuilder DefineMutatorFor
					(
						PropertyBuilder property
					)
					=> type.DefineMethod
						(
							"set_" + property.Name,
							PropertyCreation.MethodAttributes,
							property.PropertyType,
							parameterTypes: []
						);
			}

		public static void ImplementMutatorWith
			(
				this MethodBuilder method,
				FieldInfo field
			)
			{
				var il = method.GetILGenerator();
				il.Emit(OpCodes.Ldarg_0);
				il.Emit(OpCodes.Ldfld, field);
				il.Emit(OpCodes.Callvirt, typeof(Func<>).GetMethod("Invoke").NotNull());
				il.Emit(OpCodes.Ldarg_1);
				il.Emit(OpCodes.Callvirt, method);
				il.Emit(OpCodes.Ret);
			}
	}