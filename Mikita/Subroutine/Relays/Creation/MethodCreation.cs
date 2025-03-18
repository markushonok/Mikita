using Mikita.Assertion;
using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Mikita.Subroutine.Relays.Creation;

public static class MethodCreation
	{
		public static void CreateMethodWith<T>
			(
				this TypeBuilder type,
				MethodInfo reference,
				FieldInfo field
			)
			{
				type
					.DefineMethodBasedOn(reference)
					.ImplementWith<T>(reference, field);
			}

		public static MethodBuilder DefineMethodBasedOn
			(
				this TypeBuilder type,
				MethodInfo reference
			)
			{
				return type.DefineMethod
					(
						reference.Name,
						RelayMethodAttributes,
						reference.ReturnType,
						parameterTypes: reference
							.GetParameters()
							.Select(x => x.ParameterType)
							.ToArray()
					);
			}

		public static void ImplementWith<T>
			(
				this MethodBuilder method,
				MethodInfo reference,
				FieldInfo field
			)
			{
				var il = method.GetILGenerator();
				il.Emit(OpCodes.Ldarg_0);
				il.Emit(OpCodes.Ldfld, field);
				il.EmitCall(OpCodes.Callvirt, typeof(Func<T>).GetMethod("Invoke").NotNull(), optionalParameterTypes: null);

				var parameterCount = reference.GetParameters().Length;

				for (var i = 0; i < parameterCount; i++)
					il.Emit(OpCodes.Ldarg, i + 1);

				il.EmitCall(OpCodes.Callvirt, reference, optionalParameterTypes: null);
				il.Emit(OpCodes.Ret);
			}

		public const MethodAttributes RelayMethodAttributes 
			= MethodAttributes.Public
			| MethodAttributes.HideBySig
			| MethodAttributes.Final 
			| MethodAttributes.Virtual;
	}