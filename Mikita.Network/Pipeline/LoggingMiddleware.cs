using Mikita.Logging;
using Mikita.Network.Messages;
using Mikita.Network.Messages.Registries;
using Mikita.Network.Peers.Keys;
using System;
using System.Buffers.Binary;

namespace Mikita.Network.Pipeline;

public static class LoggingMiddlewareInstancing
	{
		extension(Middleware)
			{
				public static IMiddleware Logging
					(
						IMessageRegistry messages,
						ILog log,
						string context
					)
					=> new LoggingMiddleware(messages, log, context);
			}
	}

/// <summary>
/// Reports a complete packet that a middleware chain did not accept.
/// Reads request correlation only for registered requests and responses.
/// </summary>
/// <remarks>
/// Intended for the terminal branches of a router, so a dropped
/// packet leaves a diagnostic instead of failing silently.
/// </remarks>
/// <param name="messages">Resolves packet roles.</param>
/// <param name="log">Receives the diagnostic line.</param>
/// <param name="context">Names the branch that dropped the packet.</param>
public sealed class LoggingMiddleware
	(
		IMessageRegistry messages,
		ILog log,
		string context
	)
	: IMiddleware
	{
		public void Handle
			(
				PeerKey peer,
				ReadOnlySpan<byte> bytes
			)
			{
				var typeId = TypeIdFrom(bytes);
				var label = typeId is {} value ? messages.LabelOf(value) : null;

				log.Write
					(
						$"{context}: peer={peer},"
						+ $" typeId={typeId?.Value.ToString() ?? "unknown"},"
						+ $" role={label?.Role.ToString() ?? "unknown"},"
						+ $" request={RequestFrom(bytes, label?.Role)},"
						+ $" size={bytes.Length}"
					);
			}

		private static MessageTypeId? TypeIdFrom(ReadOnlySpan<byte> bytes)
			=> bytes.Length >= sizeof(short)
				? new MessageTypeId(BinaryPrimitives.ReadInt16LittleEndian(bytes))
				: null;

		private static string RequestFrom
			(
				ReadOnlySpan<byte> bytes,
				MessageRole? role
			)
			=> role switch
				{
					MessageRole.Notice => "none",
					MessageRole.Request or MessageRole.Response
						when bytes.Length >= sizeof(short) + sizeof(int)
						=> BinaryPrimitives
							.ReadInt32LittleEndian(bytes[sizeof(short)..])
							.ToString(),
					_ => "unknown"
				};
	}