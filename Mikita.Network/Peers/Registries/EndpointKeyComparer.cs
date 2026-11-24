using System;
using System.Collections.Generic;
using System.Net;

namespace Mikita.Network.Peers.Registries;

/// <summary>
/// Compares endpoints by address bytes and port.
/// </summary>
/// <remarks>
/// <see cref="IPEndPoint.GetHashCode"/> is not consistent with
/// <see cref="IPEndPoint.Equals"/> for equal addresses produced by
/// different sources, such as DNS resolution and a received packet.
/// A registry keyed by the default comparer then fails to find an
/// endpoint it already holds and registers a second key for it.
/// </remarks>
public sealed class EndpointKeyComparer
	: IEqualityComparer<IPEndPoint>
	{
		public static EndpointKeyComparer Instance { get; }
			= new();

		public bool Equals
			(
				IPEndPoint? left,
				IPEndPoint? right
			)
			=> left is null
				? right is null
				: left.Equals(right);

		public int GetHashCode
			(
				IPEndPoint endPoint
			)
			{
				var hash = new HashCode();
				hash.Add(endPoint.Port);

				foreach (var @byte in endPoint.Address.GetAddressBytes())
					hash.Add(@byte);

				return hash.ToHashCode();
			}
	}