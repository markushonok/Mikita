using System;
using TLive = Mikita.Evaluation.Liveness.Live;

namespace Mikita.Evaluation.Liveness;

public static class LiveMembership
	{
		public static TTarget Live
			<
				TSource,
				TTarget
			>
			(
				this TSource source,
				Func<TSource, TTarget> pattern
			)
			where TTarget: class
			=> TLive.ResultOf(() => pattern(source));
	}