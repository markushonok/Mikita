using Mikita.Observation.Events;
using Mikita.Structs.Referring;

namespace Mikita.Observation.Change;

public static class ObservedInstancing
	{
		extension<T>(T value)
			{
				public Shown<T> AsObserved
					=> new(Ref.To(value), Event.NewEmpty);
			}
	}