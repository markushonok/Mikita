using Godot;

namespace Mikita.Godot.Nodes.Cameras3D;

public static class RayCast
	{
		public static Vector3? RayCastPoint
			(
				this Camera3D camera,
				Vector2 screenPoint,
				float distance = 1000
			)
			{
				var from = camera.ProjectRayOrigin(screenPoint);
				var to = from + camera.ProjectRayNormal(screenPoint) * distance;

				var spaceState = camera.GetWorld3D().DirectSpaceState;
				var query = PhysicsRayQueryParameters3D.Create(from, to);
				var result = spaceState.IntersectRay(query);

				return (Vector3) result.GetValueOrDefault("position");
			}
	}