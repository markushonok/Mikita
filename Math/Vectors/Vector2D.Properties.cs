namespace Mikita.Math.Vectors;

public partial interface Vector2D<out T>
	{
		T Length
			=> this.Length();

		Vector2D<T> Sign
			=> this.Sign();

		Vector2D<T> Normal
			=> this.Normal();
	}