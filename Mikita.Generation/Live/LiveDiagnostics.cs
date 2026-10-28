using Microsoft.CodeAnalysis;

namespace Mikita.Generation.Live;

public static class LiveDiagnostics
	{
		public static readonly DiagnosticDescriptor LiveRequiresInterface
			= Descriptor
				(
					id: "MIK0007",
					title: "The live implementation requires an interface",
					message: "The live implementation of '{0}' requires"
						+ " an interface"
				);

		public static readonly DiagnosticDescriptor LiveMemberUnsupported
			= Descriptor
				(
					id: "MIK0008",
					title: "The live implementation cannot relay a member",
					message: "The live implementation of '{0}' cannot relay"
						+ " '{1}': {2}"
				);

		public static readonly DiagnosticDescriptor LiveRelayMissing
			= Descriptor
				(
					id: "MIK0009",
					title: "The live relay registry is missing",
					message: "The type"
						+ " 'Mikita.Evaluation.Liveness.LiveRelay<T>' cannot"
						+ " be found. Reference Mikita from this project."
				);

		public static readonly DiagnosticDescriptor ModuleInitializerMissing
			= Descriptor
				(
					id: "MIK0010",
					title: "Module initializers are unavailable",
					message: "The type"
						+ " 'System.Runtime.CompilerServices"
						+ ".ModuleInitializerAttribute' cannot be found."
						+ " The live relay cannot register itself."
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