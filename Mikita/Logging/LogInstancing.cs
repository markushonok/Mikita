using System.Collections;
using System.Collections.Generic;

namespace Mikita.Logging;

public static class LogInstancing
	{
		extension(Log)
			{
				public static ILog Debug
					=> DebugLog.Instance;

				public static MultiLog With
					(
						IEnumerable<ILog> logs
					)
					=> new(logs);
			}
	}