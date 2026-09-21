using System.Threading;

namespace Mikita.Logging;

/// <summary>
/// Serializes access to another multi-log.
/// </summary>
/// <remarks>
/// All access to the source must pass through this instance.
/// </remarks>
public sealed class SerialAccessMultiLog
	(
		IMultiLog source,
		Lock access
	)
	: IMultiLog
	{
		public void Write(string @string)
			{
				lock (access) source.Write(@string);
			}

		public void Add(ILog log)
			{
				lock (access) source.Add(log);
			}

		public void Remove(ILog log)
			{
				lock (access) source.Remove(log);
			}
	}