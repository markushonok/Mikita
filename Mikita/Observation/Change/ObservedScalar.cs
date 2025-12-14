using Mikita.Observation.Events;
using Mikita.Structs.Scalars;
using System;

namespace Mikita.Observation.Change;

public sealed partial class ObservedScalar<T>
	(
		Scalar<T> current,
		IEvent<Action> changed
	) 
	: IManaged<T>
	{
		public T Current
			{
				get => current.Value;
				set => current.Value = value;
			}

		public event Action Changed
			{
				add => changed.Add(value);
				remove => changed.Remove(value);
			}
	}