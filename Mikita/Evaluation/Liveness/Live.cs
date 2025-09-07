using Mikita.Evaluation.Liveness.Creation;
using Mikita.Nulls;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mikita.Evaluation.Liveness;

public static partial class Live
	{
		public static T ResultOf<T>(Func<T> function)
			where T: class
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