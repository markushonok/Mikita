using Mikita.Structs.Enumerables;
using System.Collections.Generic;

namespace Mikita.Logging;

public sealed class MultiLog
	(
		ICollection<ILog> logs
	)
	: IMultiLog
	{
		public void Write(string @string)
			=> logs.ForEach(x => x.Write(@string));

		public void Add(ILog log)
			=> logs.Add(log);

		public void Remove(ILog log)
			=> logs.Remove(log);
	}