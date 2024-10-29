using Mikita.NullSafety;
using System.Reflection;
using System.Reflection.Emit;

namespace Mikita.Subroutine.Relays.Creation.Properties;

public static class MutatorCreation
	{
		public static void CreateMutatorFor
			(
				this TypeBuilder type,
				PropertyBuilder property,
				FieldInfo field
			)
			{
				var method = type.DefineMutatorFor(property);
				method.ImplementMutatorWith(field);
				property.SetSetMethod(method);
			}

		public static MethodBuilder DefineMutatorFor
			(
				this TypeBuilder type,
				PropertyBuilder property
			)
			{
				return type.DefineMethod
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