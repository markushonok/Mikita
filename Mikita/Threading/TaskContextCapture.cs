using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Mikita.Threading;

public static class TaskContextCapture
	{
		public static ConfiguredTaskAwaitable ContextFree
			(
				this Task source
			)
			=> source.ConfigureAwait(continueOnCapturedContext: false);

		public static ConfiguredTaskAwaitable<T> ContextFree<T>
			(
				this Task<T> source
			)
			=> source.ConfigureAwait(continueOnCapturedContext: false);
	}