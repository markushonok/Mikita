using System;

namespace Mikita.Network.Peers.Keys;

public readonly struct PeerKey
	(
		int number
	)
	: IPeerKey, IEquatable<PeerKey>
	{
		public int Number
			=> number;

		public static bool operator ==
			(
				PeerKey left,
				PeerKey right
			)
			=> left.Equals(right);

		public static bool operator !=
			(
				PeerKey left,
				PeerKey right
			)
			=> !left.Equals(right);

		public override bool Equals
			(
				object? @object
			)
			=> @object is PeerKey other && Equals(other);

		public bool Equals
			(
				PeerKey other
			)
			=> number.Equals(other.Number);

		public override int GetHashCode()
			=> number.GetHashCode();

		public override string ToString()
			=> number.ToString();
	}