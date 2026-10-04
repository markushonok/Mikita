using System.Collections.Generic;

namespace Mikita.Steps.Marching;

/// <summary>
/// Provides concurrent step compositions.
/// </summary>
public static class March
	{
		public static IAsyncStep Of
			(
				IEnumerable<IAsyncStep> steps
			)
			=> new AsyncMarch(steps);
	}