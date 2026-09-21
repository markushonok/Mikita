using System.Threading;

namespace Mikita.Logging;

/// <summary>
/// Provides standard multi-log compositions.
/// </summary>
public static class MultiLogInstancing
	{
		extension(MultiLog)
			{
				/// <summary>
				/// A multi-log with no logs.
				/// </summary>
				public static IMultiLog NewEmpty
					=> new MultiLog([]);
			}

		extension(IMultiLog log)
			{
				/// <summary>
				/// Adds serialized access. The source must no longer be accessed
				/// directly.
				/// </summary>
				public IMultiLog WithSerialAccess
					=> new SerialAccessMultiLog
						(
							log,
							new Lock()
						);
			}
	}