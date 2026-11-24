using System;

namespace Mikita.Network.Peers.Channels;

/// <summary>
/// Identifies a peer channel by a
/// process-locally issued number.
/// </summary>
public readonly struct PeerChannelId
	(
		int number
	)
	: IEquatable<PeerChannelId>
	{
		public int Number
			=> number;

		public static bool operator ==
			(
				PeerChannelId left,
				PeerChannelId right
			)
			=> left.Equals(right);

		public static bool operator !=
			(
				PeerChannelId left,
				PeerChannelId right
			)
			=> !left.Equals(right);

		public override bool Equals
			(
				object? @object
			)
			=> @object is PeerChannelId other && Equals(other);

		public bool Equals
			(
				PeerChannelId other
			)
			=> number.Equals(other.Number);

		public override int GetHashCode()
			=> number.GetHashCode();

		public override string ToString()
			=> number.ToString();
	}