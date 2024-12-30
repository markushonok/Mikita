using System.Reflection;
using System.Reflection.Emit;

namespace Mikita.Reflection.Members;

public static class FieldReflection
	{
		public static FieldBuilder DefineField<T>
			(
				this TypeBuilder target,
				string name,
				FieldAttributes attributes
			)
			=> target.DefineField(name, typeof(T), attributes);
	}