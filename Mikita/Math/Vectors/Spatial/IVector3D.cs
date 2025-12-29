namespace Mikita.Math.Vectors.Spatial;

public interface IVector3D<out T>
	{
		T X { get; }
		
		T Y { get; }
		
		T Z { get; }
	}