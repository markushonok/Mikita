using Mikita.Structs.Scalars;

namespace Mikita.Observation.Change;

public partial class ObservedSource<T>
	{
		public RelayScalar<T> AsScalar
			=> this.AsScalar();
	}