using System;

namespace Mikita.Logging;

public static class LogWriting
	{
		extension(ILog log)
			{
				public void Write(Exception exception)
					=> log.Write(exception.Message);
			}
	}