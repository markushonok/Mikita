namespace Mikita.Structs.Referring;

public static class BoolToggling
	{
		extension(IRef<bool> boolean)
			{
				public void Toggle
					()
					=> boolean.Value = !boolean.Value;

				public void TurnTrue
					()
					=> boolean.Value = true;

				public void TurnFalse
					()
					=> boolean.Value = false;
			}
	}