using System.Collections.Generic;

namespace Mikita.Generation.Inference.Nodes;

public sealed record Node
	(
		string Name, 
		IEnumerable<string> TypeParameters
	)
	: INode;