namespace Mikita.Observing;

public static class BackCallingAssign
	{
		public static void AssignTo<T>
			(
				this T value,
				ref T field,
				Action then
			)
			{
				field = value;
				then();
			}
	}