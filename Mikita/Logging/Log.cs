using System.Collections.Generic;

namespace Mikita.Logging;

public static class Log
	{
		public static ILog Debug
			=> DebugLog.Instance;

		public static MultiLog With
			(
				ICollection<ILog> logs
			)
			=> new(logs);
	}