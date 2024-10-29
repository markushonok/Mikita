namespace Mikita.Graphics.Colors;

public record struct ColorRecord<T>
	(
		T Red, 
		T Green, 
		T Blue, 
		T Alpha
	) : Color<T>;