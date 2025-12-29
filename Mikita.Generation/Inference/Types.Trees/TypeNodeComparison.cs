namespace Mikita.Generation.Inference.Types.Trees;

public static class TypeNodeComparison
	{
		extension(ITypeNode node)
			{
				public bool IsTypeParameter
					=> !node.IsSpecificType;

				public bool IsSpecificType
					=> node.Name.StartsWith("global::");
			}
	}