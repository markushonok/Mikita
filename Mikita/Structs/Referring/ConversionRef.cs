using System;

namespace Mikita.Structs.Referring;

public sealed class ConversionRef
	<
		TSource, TResult
	>
	(
		IRef<TSource> source,
		Func<TSource, TResult> resultFrom,
		Func<TResult, TSource> sourceFrom
	)
	: IRef<TResult>
	where TSource: notnull
	{
		public TResult Value
			{
				get => resultFrom(source.Value);
				set => source.Value = sourceFrom(value);
			}
	}