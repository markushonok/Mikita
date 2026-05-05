using Mikita.Objects.Tokens;

namespace Mikita.Structs.Referring;

public sealed class Ref<T>
	(
		T referent
	)
	: IRef<T>
	{
		public T Value
			{
				get => referent;
				set => referent = value;
			}

		public override bool Equals
			(
				object? @object
			)
			=> Object.Equals(@object);

		public override int GetHashCode()
			=> Object.GetHashCode();

		public override string? ToString()
			=> referent?.ToString();

		private ObjectToken<IRef<T>> Object
			=> ObjectToken.Of(this, Members);

		private static readonly MemberToken<IRef<T>>[] Members
			= [IRef<T>.Member(x => x.Value)];
	}

public static class Ref;