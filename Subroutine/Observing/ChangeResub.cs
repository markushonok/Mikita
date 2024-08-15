namespace Mikita.Subroutine.Observing;

public static class ChangeResub
	{
		public static void ResubIn<T>
			(
				this T value,
				ref T field,
				Action changing,
				Action changed
			)
			where T : ObservedChange
			{
				field.Changing -= changing;
				field.Changed -= changed;
				field = value;
				field.Changing += changing;
				field.Changed += changed;
			}
		
		public static void ResubIn<T>
			(
				this T value,
				ref T field,
				Action changed
			)
			where T : ObservedChange
			{
				field.Changed -= changed;
				field = value;
				field.Changed += changed;
			}
		
		public static void ResubChangingIn<T>
			(
				this T value,
				ref T field,
				Action reaction
			)
			where T : ObservedChange
			{
				field.Changing -= reaction;
				field = value;
				field.Changing += reaction;
			}
	}