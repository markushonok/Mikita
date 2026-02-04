using Godot;

namespace Mikita.Godot.Graphics;

public static class TextureCaching
	{
		extension(Texture2D texture)
			{
				public ImageTexture Snapshot
					=> ImageTexture.CreateFromImage(texture.GetImage());
			}
	}