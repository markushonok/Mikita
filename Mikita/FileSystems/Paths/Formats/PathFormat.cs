using Mikita.Structs.Collections;
using Mikita.Structs.Queues;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.FileSystems.Paths.Formats;

public sealed partial class PathFormat
	(
		string separator
	)
	: IPathFormat
	{
		public void ApplyTo
			(
				IPath path,
				out string formatted
			)
			{
				var elements = Enumerable
					.Repeat("..", path.Ascends)
					.Concat(path.Elements);

				formatted = string.Join(separator, elements);
			}

		public void Parse(string formatted, out IPath path)
			{
				var elements = formatted
					.Split(separator)
					.Except(["."])
					.ToQueue();

				var paths = new List<Path>();

				while (elements.IsNotEmpty())
					{
						var ascends = elements
							.DequeueWhile(x => x == "..")
							.Count();

						var objects = elements
							.DequeueUntil(x => x == "..")
							.ToArray();

						paths.Add(new Path(objects, ascends));
					}

				path = Path.From(paths);
			}

		public bool IsSupport(string formatted)
			=> formatted.Contains(separator);
	}