using Mikita.Structs.Enumerables;
using System.Collections.Generic;

namespace Mikita.Logging;

public sealed class MultiLog
	(
		IEnumerable<ILog> logs
	)
	: ILog
	{
		public void Write(string @string)
			=> logs.ForEach(x => x.Write(@string));
	}