namespace Mikita.Logging;

public sealed class ConsoleLog: ILog
	{
		public void Write(string @string)
			=> System.Console.WriteLine(@string);
	}