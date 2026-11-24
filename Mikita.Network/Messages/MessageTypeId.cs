namespace Mikita.Network.Messages;

/// <summary>
/// A two-byte message type number assigned by registration order. It is
/// meaningful only with the same ordered registrations at both endpoints.
/// </summary>
public readonly record struct MessageTypeId(short Value);