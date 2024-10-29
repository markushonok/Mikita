using Mikita.Many.Scalars;

namespace Mikita.Observing;

public interface ObservedSource<T>
	: Observed<T>, Scalar<T>
	{
		new T Value { get; set; }
	}