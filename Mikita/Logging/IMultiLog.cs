namespace Mikita.Logging;

/// <summary>
/// An <see cref="ILog"/> that fans out to a mutable set of logs.
/// </summary>
public interface IMultiLog: ILog
	{
		void Add(ILog log);

		void Remove(ILog log);
	}