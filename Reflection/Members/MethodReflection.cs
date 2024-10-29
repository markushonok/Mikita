using System.Reflection;

namespace Mikita.Reflection.Members;

public static class MethodReflection
	{
		public static IEnumerable<MethodInfo> AllMethods
			(
				this Type type,
				BindingFlags flags
			)
			{
				var ancestral = type.MethodsOfAncestralInterfaces(flags);
				return type.OwnMethods(flags).Concat(ancestral);
			}
		
		public static IEnumerable<MethodInfo> MethodsOfAncestralInterfaces
			(
				this Type type,
				BindingFlags flags
			)
			{
				return type
					.AncestralInterfaces()
					.SelectMany(@interface => @interface.OwnMethods(flags));
			}
		
		/// <inheritdoc cref="Type.GetMethods(BindingFlags)"/>
		public static MethodInfo[] OwnMethods
			(
				this Type type,
				BindingFlags flags
			)
			=> type.GetMethods(flags);
		
		/// <inheritdoc cref="Type.GetMethods()"/>
		public static MethodInfo[] OwnMethods
			(
				this Type type
			)
			=> type.GetMethods();
		
	}