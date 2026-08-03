using System.Collections.Generic;

namespace Mikita.Logging;

public static class Log
	{
		public static ILog Debug
			=> DebugLog.Instance;

		public static MultiLog With
			(
				IEnumerable<ILog> logs
			)
			=> new(logs);
	}