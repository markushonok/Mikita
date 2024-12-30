namespace Mikita.Measurement.Lengths;

public partial interface Length<out T>
	{
		T InKilometers =>
			this.InKilometers();

		T InCentimeters =>
			this.InCentimeters();

		T InMillimeters =>
			this.InMillimeters();
	}