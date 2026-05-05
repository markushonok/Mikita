using Mikita.Structs.Referring;

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