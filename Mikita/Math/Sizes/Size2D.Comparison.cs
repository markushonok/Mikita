using Mikita.Objects.Tokens;

namespace Mikita.Math.Sizes;

partial class Size2D<T>
	{
		public override bool Equals
			(
				object? @object
			)
			=> Object.Equals(@object);

		public override int GetHashCode()
			=> Object.GetHashCode();

		private ObjectToken<ISize2D<T>> Object
			=> ObjectToken.Of(this, Members);

		private static readonly MemberToken<ISize2D<T>>[] Members
			=
				[
					ISize2D<T>.Member(x => x.X),
					ISize2D<T>.Member(x => x.Y)
				];
	}