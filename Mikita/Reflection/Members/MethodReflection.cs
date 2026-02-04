using Mikita.Structs.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mikita.Reflection.Members;

public static class MethodReflection
	{
		extension(Type type)
			{
				public IEnumerable<MethodInfo> AllMethods
					(
						BindingFlags flags
					)
					{
						var ancestral = type.MethodsOfAncestralInterfaces(flags);
						return type
							.OwnMethods(flags)
							.Concat(ancestral)
							.Concat(typeof(object).OwnMethods(flags));
					}

				public IEnumerable<MethodInfo> MethodsOfAncestralInterfaces
					(
						BindingFlags flags
					)
					{
						return type
							.AncestralInterfaces()
							.SelectMany(@interface => @interface.OwnMethods(flags));
					}

				/// <inheritdoc cref="Type.GetMethods(BindingFlags)"/>
				public MethodInfo[] OwnMethods
					(
						BindingFlags flags
					)
					=> type.GetMethods(flags);

				/// <inheritdoc cref="Type.GetMethods()"/>
				public MethodInfo[] OwnMethods()
					=> type.GetMethods();
			}
	}