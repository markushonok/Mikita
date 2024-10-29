using Mikita.NullSafety;
using Mikita.Subroutine.Relays.Creation;
using System.Linq.Expressions;

namespace Mikita.Subroutine.Relays;

public static partial class Relay
	{
		public static T To<T>(Func<T> function)
			where T : class
			{
				if (Constructors.TryGetValue(typeof(T), out var @object)) 
					return ((RelayConstructor<T>) @object)(function);

				var implementation = TypeCreation.Create<T>();
				var constructor = CompileConstructorFor<T>(implementation);
				Constructors[typeof(T)] = constructor;
				return constructor(function);
			}

		private static RelayConstructor<T> CompileConstructorFor<T>(Type type)
			{
				var parameter = Expression.Parameter(typeof(Func<T>), "value");
				var constructor = type.GetConstructor([typeof(Func<T>)]).NotNull();
				return Expression.Lambda
					<
						RelayConstructor<T>
					>
					(
						Expression.New(constructor, parameter),
						parameter
					)
					.Compile();
			}

		private static readonly 
			Dictionary<Type, object> Constructors
			= new();
	}