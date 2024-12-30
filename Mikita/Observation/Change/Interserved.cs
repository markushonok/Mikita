namespace Mikita.Observation.Change;

public partial interface Interserved<T>
	: Observed<T>
	{
		new T Current { get; set; }

	}