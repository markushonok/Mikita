using Mikita.Routines;
using Mikita.Structs.Referring;
using System;
using System.Threading.Tasks;

namespace Mikita.Caching;

public static class Valid
	{
		public static AsyncValid<T> From<T>
			(
				CancellableTask<T> fresh
			)
			=> new
				(
					fresh,
					current: Ref<Task<T>>.Null,
					isValid: Ref.To(false)
				);

		extension<T>(IAsyncValid<T> source)
			{
				public AsyncRetryingValid<T> WithRetries
					(
						int count,
						TimeSpan interval = default
					)
					=> new
						(
							source,
							() => count,
							interval
						);
			}
	}