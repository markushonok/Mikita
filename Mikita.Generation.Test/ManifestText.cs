using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace Mikita.Generation.Test;

public sealed class ManifestText
	(
		string text
	)
	: AdditionalText
	{
		public override string Path
			=> "Implementations.toml";

		public override SourceText GetText
			(
				CancellationToken cancel = default
			)
			=> SourceText.From(text, Encoding.UTF8);
	}