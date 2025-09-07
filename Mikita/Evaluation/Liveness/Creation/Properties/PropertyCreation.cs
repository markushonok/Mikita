using System.Reflection;
using System.Reflection.Emit;
using static System.Reflection.MethodAttributes;

namespace Mikita.Evaluation.Liveness.Creation.Properties;

public static class PropertyCreation
	{
		public static void CreatePropertyWith
			(
				this TypeBuilder type,
				PropertyInfo reference,
				FieldInfo field
			)
			{
				var property = type.DefinePropertyFrom(reference);
				type.Implement(property, reference, field);
			}
		
		public static PropertyBuilder DefinePropertyFrom
			(
				this TypeBuilder type,
				PropertyInfo reference
			)
			{
				return type.DefineProperty
					(
						reference.Name,
						reference.Attributes,
						reference.PropertyType,
						parameterTypes: []
					);
			}
		
		public static void Implement
			(
				this TypeBuilder type,
				PropertyBuilder property,
				PropertyInfo reference,
				FieldInfo field
			)
			{
				if (reference.CanRead)
					type.CreateAccessorFor(property, field);
				
				if (reference.CanWrite)
					type.CreateMutatorFor(property, field);
			}

		public static MethodAttributes MethodAttributes
			=> Public 
			 | Final 
			 | Virtual
			 | SpecialName
			 | HideBySig;
	}