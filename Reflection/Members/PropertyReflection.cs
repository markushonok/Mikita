using System.Reflection;

namespace Mikita.Reflection.Members;

public static class PropertyReflection
	{
		public static IEnumerable<PropertyInfo> AllProperties
			(
				this Type type,
				BindingFlags flags
			)
			{
				var ancestral = type.PropertiesOfAncestralInterfaces(flags);
				return type.OwnProperties(flags).Concat(ancestral);
			}
		
		public static IEnumerable<PropertyInfo> PropertiesOfAncestralInterfaces
			(
				this Type type,
				BindingFlags flags
			)
			{
				return type
					.AncestralInterfaces()
					.SelectMany(@interface => @interface.OwnProperties(flags));
			}
		
		/// <inheritdoc cref="Type.GetProperties(BindingFlags)"/>
		public static PropertyInfo[] OwnProperties
			(
				this Type type,
				BindingFlags flags
			)
			=> type.GetProperties(flags);
		
		/// <inheritdoc cref="Type.GetProperties()"/>
		public static PropertyInfo[] OwnProperties
			(
				this Type type
			)
			=> type.GetProperties();
		
	}