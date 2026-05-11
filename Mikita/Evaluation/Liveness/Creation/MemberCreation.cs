using Mikita.Evaluation.Liveness.Creation.Properties;
using Mikita.Reflection.Members;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Mikita.Evaluation.Liveness.Creation;

public static class MemberCreation
	{
		extension(TypeBuilder type)
			{
				public void CreateMembers<T>()
					{
						var field = type.DefineField<T>();
						type.CreateConstructorWith<T>(field);
				
						foreach (var reference in typeof(T).AllMethods(Flags))
							type.CreateMethodWith<T>(reference, field);

						foreach (var reference in typeof(T).AllProperties(Flags))
							type.CreatePropertyWith(reference, field);

						type.CreateObjectOverridesWith<T>(field);
					}

				public void CreateObjectOverridesWith<T>(FieldInfo field)
					{
						type.CreateMethodWith<T>(typeof(object).GetMethod(nameof(ToString), [])!, field);
						type.CreateMethodWith<T>(typeof(object).GetMethod(nameof(GetHashCode), [])!, field);
						type.CreateMethodWith<T>(typeof(object).GetMethod(nameof(Equals), [typeof(object)])!, field);
					}

				public FieldBuilder DefineField<T>()
					=> type.DefineField<Func<T>>
						(
							"function",
							FieldAttributes.Private
						);
			}

		public static BindingFlags Flags
			=> BindingFlags.Instance
				| BindingFlags.Public;
	}