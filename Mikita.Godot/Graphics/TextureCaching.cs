using Godot;

namespace Mikita.Godot.Graphics;

public static class TextureCaching
	{
		public static ImageTexture Snapshot
			(
				this Texture2D texture
			)
			=> ImageTexture.CreateFromImage(texture.GetImage());
	}