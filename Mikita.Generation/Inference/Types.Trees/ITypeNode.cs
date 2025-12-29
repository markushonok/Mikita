using System.Collections.Generic;

namespace Mikita.Generation.Inference.Types.Trees;

public interface ITypeNode
	{
		string Name { get; }

		IEnumerable<ITypeNode> Arguments { get; }
	}