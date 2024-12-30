namespace Mikita.Math.Vectors;

public readonly partial record struct VectorRecord2D<T>
	{
		public T Length
			=> this.Length();

		public Vector2D<T> Sign
			=> this.Sign();

		public Vector2D<T> Normal
			=> this.Normal();
	}