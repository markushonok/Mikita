using Mikita.Nulls;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Mikita.Evaluation.Liveness.Creation.Properties;

public static class AccessorCreation
	{
		extension(TypeBuilder type)
			{
				public void CreateAccessorFor
					(
						PropertyBuilder property,
						FieldInfo field
					)
					{
						var method = type.DefineAccessorFor(property);
						method.ImplementAccessorWith(field);
						property.SetGetMethod(method);
					}

				public MethodBuilder DefineAccessorFor
					(
						PropertyBuilder property
					)
					=> type.DefineMethod
						(
							"get_" + property.Name,
							PropertyCreation.MethodAttributes,
							property.PropertyType,
							parameterTypes: []
						);
			}

		public static void ImplementAccessorWith
			(
				this MethodBuilder method,
				FieldInfo field
			)
			{
				var il = method.GetILGenerator();
				il.Emit(OpCodes.Ldarg_0);
				il.Emit(OpCodes.Ldfld, field);
				il.Emit(OpCodes.Callvirt, typeof(Func<>).GetMethod("Invoke").NotNull());
				il.Emit(OpCodes.Callvirt, method);
				il.Emit(OpCodes.Ret);
			}
	}