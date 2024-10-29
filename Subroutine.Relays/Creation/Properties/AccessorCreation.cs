using Mikita.NullSafety;
using System.Reflection;
using System.Reflection.Emit;

namespace Mikita.Subroutine.Relays.Creation.Properties;

public static class AccessorCreation
	{
		public static void CreateAccessorFor
			(
				this TypeBuilder type,
				PropertyBuilder property,
				FieldInfo field
			)
			{
				var method = type.DefineAccessorFor(property);
				method.ImplementAccessorWith(field);
				property.SetGetMethod(method);
			}

		public static MethodBuilder DefineAccessorFor
			(
				this TypeBuilder type,
				PropertyBuilder property
			)
			{
				return type.DefineMethod
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