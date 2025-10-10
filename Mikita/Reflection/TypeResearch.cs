using System;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.Reflection;

public static class TypeResearch
	{
		public static IEnumerable<Type> AncestralInterfaces(this Type type)
			{
				var shallow = type.ParentalInterfaces().ToArray();
				
				var deep = shallow
					.SelectMany(@interface => @interface.GetInterfaces());
				
				return shallow.Concat(deep).Distinct();
			}
		
		/// <inheritdoc cref="Type.GetInterfaces()"/>
		public static IEnumerable<Type> ParentalInterfaces(this Type type)
			=> type.GetInterfaces();
	}