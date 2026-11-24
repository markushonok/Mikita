using Mikita.Network.Peers.Registries;
using System;

namespace Mikita.Network.Peers.Keys;

public readonly struct TemporalPeerKey<T>
	(
		IPeerRegistry<T> registry,
		int number
	)
	: ITemporalPeerKey, IEquatable<TemporalPeerKey<T>>
	where T: class
	{
		public int Number
			=> number;

		public void Dispose()
			=> registry.Release(this);

		public static implicit operator PeerKey
			(
				TemporalPeerKey<T> key
			)
			=> new(key.Number);

		public static bool operator ==
			(
				TemporalPeerKey<T> left,
				TemporalPeerKey<T> right
			)
			=> left.Equals(right);

		public static bool operator !=
			(
				TemporalPeerKey<T> left,
				TemporalPeerKey<T> right
			)
			=> !left.Equals(right);

		public override bool Equals
			(
				object? @object
			)
			=> @object is TemporalPeerKey<T> other && Equals(other);

		public bool Equals
			(
				TemporalPeerKey<T> other
			)
			=> number.Equals(other.Number);

		public override int GetHashCode()
			=> number.GetHashCode();

		public override string ToString()
			=> number.ToString();
}