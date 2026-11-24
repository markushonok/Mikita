namespace Mikita.Network.Pipeline;

public static class Middleware
	{
		public static IMiddleware Idle { get; }
			= new IdleMiddleware();
	}