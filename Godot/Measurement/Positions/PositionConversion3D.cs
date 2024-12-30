using Mikita.Measurement.Positions;
using System.Numerics;
using Vector3 = Godot.Vector3;

namespace Mikita.Godot.Measurement.Positions;

public static class PositionConversion3D
	{
		public static Vector3 ToGodot<T>
			
			(this Position3D<T> position) 
			where T: INumber<T>
		
			=> new
				(
					float.CreateChecked(position.X.InMeters), 
					float.CreateChecked(position.Y.InMeters), 
					float.CreateChecked(position.Z.InMeters)
				);
	}