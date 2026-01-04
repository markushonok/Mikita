namespace Mikita.Observation.Change;

public interface IShown<T>: IObserved<T>
	{
		new T Current { get; set; }
	}