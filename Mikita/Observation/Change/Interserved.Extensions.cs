using Mikita.Structs.Scalars;

namespace Mikita.Observation.Change;

public partial interface Interserved<T>
	{
		RelayScalar<T> AsScalar
			=> this.AsScalar();
	}