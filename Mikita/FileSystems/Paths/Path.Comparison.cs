using Mikita.Objects.Tokens;

namespace Mikita.FileSystems.Paths;

partial class Path
	{
		public override bool Equals
			(
				object? other
			)
			=> Object.Equals(other);

		public override int GetHashCode()
			=> Object.GetHashCode();

		private ObjectToken<IPath> Object
			=> ObjectToken.Of(this, Members);

		private static readonly MemberToken<IPath>[] Members
			=
				[
					IPath.Member(x => x.Elements),
					IPath.Member(x => x.Ascends)
				];
	}