using Mikita.Routines.Assignment;
using System;
using System.Text.Json.Nodes;
using System.Threading;

namespace Mikita.Structs.Referring;

public static class RefInstancing
	{
		extension(Ref)
			{
				public static IRef<T> That<T>
					(
						Func<T> returns,
						Assign<T> assigns
					)
					=> new RelayRef<T>(returns, assigns);

				public static IRef<T> To<T>
					(
						T referent
					)
					=> new Ref<T>(referent);

				public static IRef<T?> To<T>
					(
						JsonObject container,
						string path
					)
					=> new JsonObjectRef<T>(container, path);
			}

		extension<T>(IRef<T> reference)
			{
				public IRef<T> WithCallback
					(
						Action<T> callback
					)
					=> new BackCallingRef<T>(reference, () => callback(reference.Value));

				public IRef<T> WithCallback
					(
						Action callback
					)
					=> new BackCallingRef<T>(reference, callback);

				public IRef<T> With
					(
						Lock access
					)
					=> new SerialRef<T>(reference, access);

				public IRef<T> WithSerialAccess
					=> reference.With(new Lock());

				public IRef<T> WithDuplicatesIgnored
					=> new IgnoringDuplicatesRef<T>(reference);

				public IRef<T> WithDuplicatesProhibited
					=> new ProhibitingDuplicatesRef<T>(reference);
			}

		extension<T>(Ref<T>)
			where T: class
			{
				public static IRef<T> Required
					=> Ref<T>.Null.NotNull();

				public static IRef<T?> Null
					=> Ref.To<T?>(null);
			}

		extension<T>(Ref<T>)
			{
				public static IRef<T?> Default
					=> Ref.To<T?>(default);
			}
	}