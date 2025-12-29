using Mikita.Generation.Inference.Nodes;
using Mikita.Generation.Inference.Targets;
using System.Collections.Generic;

namespace Mikita.Generation.Inference.Summary.Nodes;

public interface ISummaryNode: INode
	{
		IEnumerable<ITypeTarget> Targets { get; }
	}