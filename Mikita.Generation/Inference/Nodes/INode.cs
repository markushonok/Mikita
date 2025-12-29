using System.Collections.Generic;

namespace Mikita.Generation.Inference.Nodes;

public interface INode
	{
		string Name { get; }

		IEnumerable<string> TypeParameters { get; }
	}