using Mikita.Routines.Assignment;
using System;
using System.Text.Json.Nodes;

namespace Mikita.Structs.Referring;

public static class RefInstancing
	{
		extension(Ref)
			{
				public static RelayRef<T> That<T>
					(
						Func<T> returns,
						Assign<T> assigns
					)
					=> new(returns, assigns);

				public static Ref<T> To<T>
					(
						T referent
					)
					=> new(referent);

				public static JsonObjectRef<T> To<T>
					(
						JsonObject container,
						string path
					)
					=> new(container, path);
			}

		extension<T>(IRef<T> reference)
			{
				public BackCallingRef<T> WithCallback
					(
						Action<T> callback
					)
					=> new(reference, () => callback(reference.Value));

				public BackCallingRef<T> WithCallback
					(
						Action callback
					)
					=> new(reference, callback);
			}

		extension<T>(Ref<T>)
			where T: class
			{
				public static Ref<T?> Null
					=> Ref.To<T?>(null);
			}

		extension<T>(Ref<T>)
			where T: struct
			{
				public static Ref<T> Default
					=> Ref.To<T>(default);
			}
	}