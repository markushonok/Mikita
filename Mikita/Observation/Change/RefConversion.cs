using Mikita.Observation.Events;
using Mikita.Structs.Referring;
using System;

namespace Mikita.Observation.Change;

public static class RefConversion
	{
		extension<T>(IShown<T> shown)
			{
				public RelayRef<T> AsRef
					=> new
						(
							@return: () => shown.Current,
							assign: value => shown.Current = value
						);
			}
	}