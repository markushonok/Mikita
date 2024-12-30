namespace Mikita.Math.Vectors;

public partial interface Vector3D<out T>
	{
		public T Length
			=> this.Length();

		public Vector3D<T> Sign
			=> this.Sign();

		public Vector3D<T> Normal
			=> this.Normal();
	}