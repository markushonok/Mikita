using Mikita.Evaluation.Liveness;
using System;

namespace Mikita.Structs.Referring;

public static class RefConversion
	{
		extension<T>(IRef<T> reference) where T: class
			{
				public T LiveValue
					=> reference.Live(x => x.Value);
			}

		extension<T>(IRef<T> reference)
			{
				public Func<T> AsFunction
					=> () => reference.Value;
			}
	}