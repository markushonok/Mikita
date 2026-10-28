using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Mikita.Generation.Live;

public static class LiveRelayInstancing
	{
		public static ILiveRelay? Of
			(
				INamedTypeSymbol type,
				ImmutableArray<Diagnostic>.Builder diagnostics
			)
			{
				var members = MembersOf(type, diagnostics);

				return members is {} relayed
					? new LiveRelay(type, relayed)
					: null;
			}

		private static ImmutableArray<ISymbol>? MembersOf
			(
				INamedTypeSymbol type,
				ImmutableArray<Diagnostic>.Builder diagnostics
			)
			{
				var members = ImmutableArray.CreateBuilder<ISymbol>();
				var valid = true;

				foreach (var source in SourcesOf(type))
					{
						valid &= Collect
							(
								source,
								type,
								members,
								diagnostics
							);
					}

				return valid ? members.ToImmutable() : null;
			}

		private static bool Collect
			(
				INamedTypeSymbol source,
				INamedTypeSymbol type,
				ImmutableArray<ISymbol>.Builder members,
				ImmutableArray<Diagnostic>.Builder diagnostics
			)
			{
				var valid = true;

				foreach (var member in source.GetMembers())
					{
						if (member is IMethodSymbol
							{
								MethodKind: not MethodKind.Ordinary
							})
							continue;

						if (Supported(type, member, diagnostics))
							members.Add(member);
						else
							valid = false;
					}

				return valid;
			}

		private static bool Supported
			(
				INamedTypeSymbol type,
				ISymbol member,
				ImmutableArray<Diagnostic>.Builder diagnostics
			)
			=> member.IsStatic
				? Unsupported
					(
						type,
						member,
						"static interface members are not supported",
						diagnostics
					)
				: SupportedKind(type, member, diagnostics);

		private static bool SupportedKind
			(
				INamedTypeSymbol type,
				ISymbol member,
				ImmutableArray<Diagnostic>.Builder diagnostics
			)
			=> member switch
				{
					IPropertySymbol property => SupportedProperty
						(
							type,
							property,
							diagnostics
						),
					IMethodSymbol method => SupportedMethod
						(
							type,
							method,
							diagnostics
						),
					IEventSymbol @event => SupportedEvent
						(
							type,
							@event,
							diagnostics
						),
					_ => Unsupported
						(
							type,
							member,
							"the member kind is not supported",
							diagnostics
						)
				};

		private static bool SupportedProperty
			(
				INamedTypeSymbol type,
				IPropertySymbol property,
				ImmutableArray<Diagnostic>.Builder diagnostics
			)
			{
				if (property.RefKind != RefKind.None)
					return Unsupported
						(
							type,
							property,
							"ref properties are not supported",
							diagnostics
						);

				if (property.SetMethod is {IsInitOnly: true})
					return Unsupported
						(
							type,
							property,
							"init-only setters are not supported",
							diagnostics
						);

				return property.Parameters.All(x => SupportedType(x.Type))
					|| Unsupported
						(
							type,
							property,
							"the indexer parameter type is not supported",
							diagnostics
						);
			}

		private static bool SupportedMethod
			(
				INamedTypeSymbol type,
				IMethodSymbol method,
				ImmutableArray<Diagnostic>.Builder diagnostics
			)
			{
				if (method.ReturnsByRef || method.ReturnsByRefReadonly)
					return Unsupported
						(
							type,
							method,
							"ref returns are not supported",
							diagnostics
						);

				if (!SupportedType(method.ReturnType))
					return Unsupported
						(
							type,
							method,
							"the return type is not supported",
							diagnostics
						);

				return method.Parameters.All(x => SupportedType(x.Type))
					|| Unsupported
						(
							type,
							method,
							"a parameter type is not supported",
							diagnostics
						);
			}

		private static bool SupportedEvent
			(
				INamedTypeSymbol type,
				IEventSymbol @event,
				ImmutableArray<Diagnostic>.Builder diagnostics
			)
			=> SupportedType(@event.Type)
				|| Unsupported
					(
						type,
						@event,
						"the event type is not supported",
						diagnostics
					);

		private static bool SupportedType(ITypeSymbol type)
			=> type.TypeKind
				is not (TypeKind.Pointer or TypeKind.FunctionPointer);

		private static bool Unsupported
			(
				INamedTypeSymbol type,
				ISymbol member,
				string reason,
				ImmutableArray<Diagnostic>.Builder diagnostics
			)
			{
				diagnostics.Add
					(
						Diagnostic.Create
							(
								LiveDiagnostics.LiveMemberUnsupported,
								Location.None,
								type.ToDisplayString(),
								member.ToDisplayString(),
								reason
							)
					);

				return false;
			}

		private static IEnumerable<INamedTypeSymbol> SourcesOf
			(
				INamedTypeSymbol type
			)
			=> type
				.AllInterfaces
				.Prepend(type);
	}