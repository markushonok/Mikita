using System.Collections.Generic;

namespace Mikita.Logging;

public static class DebugLog
	{
		internal static ILog Instance
			=> Log.With(Logs);

		public static void Add(ILog log)
			=> Logs.Add(log);

		private static readonly List<ILog> Logs
			= [];
	}