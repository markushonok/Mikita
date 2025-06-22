namespace Mikita.Observation.Change;

public interface IManaged<T>
	: IObserved<T>
	{
		new T Current { get; set; }
	}