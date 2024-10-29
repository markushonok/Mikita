using Mikita.Observing.Events;

namespace Mikita.Observing;

public static class ObservationSwap
	{
		public static void SwapWith<T>
			(
				this T value,
				ref T field,
				Action changing,
				Action changed
			)
			where T : PrePostChangeEvent
			{
				field.Changing -= changing;
				field.Changed -= changed;
				field = value;
				field.Changing += changing;
				field.Changed += changed;
			}
		
		public static void SwapWith<T>
			(
				this T value,
				ref T field,
				Action changed
			)
			where T : PrePostChangeEvent
			{
				field.Changed -= changed;
				field = value;
				field.Changed += changed;
			}
		
		public static void SwapChangingObservationWith<T>
			(
				this T value,
				ref T field,
				Action reaction
			)
			where T : PrePostChangeEvent
			{
				field.Changing -= reaction;
				field = value;
				field.Changing += reaction;
			}
	}