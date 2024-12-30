using Mikita.Many.Scalars;

namespace Mikita.Observing.Observation;

public partial interface Interserved<T>
	: Observed<T>
	{
		new T Current { get; set; }

	}