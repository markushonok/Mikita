using Microsoft.CodeAnalysis;

namespace Mikita.Generation.Manifests;

public static class ManifestDiagnostics
	{
		public static readonly DiagnosticDescriptor ManifestUnreadable
			= Descriptor
				(
					id: "MIK0001",
					title: "Implementation manifest is unreadable",
					message: "The implementation manifest cannot be read: {0}"
				);

		public static readonly DiagnosticDescriptor ManifestInvalid
			= Descriptor
				(
					id: "MIK0002",
					title: "Implementation manifest is invalid",
					message: "The implementation manifest is invalid: {0}"
				);

		public static readonly DiagnosticDescriptor MultipleManifests
			= Descriptor
				(
					id: "MIK0003",
					title: "Several implementation manifests are present",
					message: "Only one implementation manifest is allowed"
						+ " for a project, found {0}"
				);

		public static readonly DiagnosticDescriptor TypeUnknown
			= Descriptor
				(
					id: "MIK0004",
					title: "Implementation type is unknown",
					message: "The implementation type '{0}' cannot be resolved"
				);

		public static readonly DiagnosticDescriptor TypeNotDeclaredHere
			= Descriptor
				(
					id: "MIK0005",
					title: "Implementation type is declared in"
						+ " another assembly",
					message: "The implementation type '{0}' is declared in"
						+ " '{1}'. Declare it in the manifest of that"
						+ " assembly."
				);

		public static readonly DiagnosticDescriptor TypeDuplicated
			= Descriptor
				(
					id: "MIK0006",
					title: "Implementation type is declared twice",
					message: "The implementation type '{0}' is declared"
						+ " more than once"
				);

		private static DiagnosticDescriptor Descriptor
			(
				string id,
				string title,
				string message
			)
			=> new
				(
					id: id,
					title: title,
					messageFormat: message,
					category: Category,
					defaultSeverity: DiagnosticSeverity.Error,
					isEnabledByDefault: true
				);

		private const string Category
			= "Mikita.Generation";
	}