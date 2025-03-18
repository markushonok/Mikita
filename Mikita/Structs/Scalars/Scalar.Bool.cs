namespace Mikita.Structs.Scalars;

public static partial class Scalar
	{
		public static void Toggle
			(
				this Scalar<bool> boolean
			)
			=> boolean.Value = !boolean.Value;

		public static void TurnTrue
			(
				this Scalar<bool> boolean
			)
			=> boolean.Value = true;

		public static void TurnFalse
			(
				this Scalar<bool> boolean
			)
			=> boolean.Value = false;
	}