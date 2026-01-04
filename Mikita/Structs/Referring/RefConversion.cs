using Mikita.Evaluation.Liveness;
using System;

namespace Mikita.Structs.Referring;

public static class RefConversion
	{
		public static TTarget LiveValue
			<
				TSource,
				TTarget
			>
			(
				this IRef<TSource> reference,
				Func<TSource, TTarget> pattern
			)
			where TTarget: class
			=> Live.ResultOf(() => pattern(reference.Value));

		public static T LiveValue<T>
			(
				this IRef<T> reference
			)
			where T: class
			=> reference.Live(x => x.Value);

		public static Func<T> AsFunction<T>
			(
				this IRef<T> reference
			)
			=> () => reference.Value;
	}