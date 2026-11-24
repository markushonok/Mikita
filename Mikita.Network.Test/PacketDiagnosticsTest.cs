using Mikita.Logging;
using Mikita.Network.Messages;
using Mikita.Network.Messages.Registries;
using Mikita.Network.Peers.Keys;
using Mikita.Network.Pipeline;
using System.Buffers.Binary;

namespace Mikita.Network.Test;

public static class PacketDiagnosticsTest
	{
		[Theory]
		[InlineData(MessageRole.Request)]
		[InlineData(MessageRole.Response)]
		public static void CorrelatedPacketReportsRequest(MessageRole role)
			{
				var messages = MessageRegistry.New;
				var label = messages.Register
					(
						new MessageLabel
							(
								TypeName: "correlated",
								role
							)
					);
				var typeId = label.TypeId;
				var entries = new List<string>();
				var diagnostic = Middleware.Logging
					(
						messages,
						new CaptureLog(entries),
						context: "Rejected packet"
					);

				diagnostic.Handle
					(
						new PeerKey(7),
						PacketWith(typeId, payload: 123)
					);

				var entry = entries.ShouldHaveSingleItem();
				entry.ShouldContain("typeId=0");
				entry.ShouldContain($"role={role}");
				entry.ShouldContain("request=123");
			}

		[Fact]
		public static void NoticePayloadIsNotRequestCorrelation()
			{
				var messages = MessageRegistry.New;
				var label = messages.Register
					(
						new MessageLabel
							(
								TypeName: "changed",
								MessageRole.Notice
							)
					);
				var typeId = label.TypeId;
				var entries = new List<string>();
				var diagnostic = Middleware.Logging
					(
						messages,
						new CaptureLog(entries),
						context: "Unknown notice"
					);

				diagnostic.Handle
					(
						new PeerKey(7),
						PacketWith(typeId, payload: 456)
					);

				var entry = entries.ShouldHaveSingleItem();
				entry.ShouldContain("role=Notice");
				entry.ShouldContain("request=none");
				entry.ShouldNotContain("456");
			}

		[Fact]
		public static void UnknownTypeDoesNotAssumeRequestHeader()
			{
				var entries = new List<string>();
				var diagnostic = Middleware.Logging
					(
						MessageRegistry.New,
						new CaptureLog(entries),
						context: "Unknown type"
					);

				diagnostic.Handle
					(
						new PeerKey(7),
						PacketWith(new MessageTypeId(9), payload: 789)
					);

				var entry = entries.ShouldHaveSingleItem();
				entry.ShouldContain("typeId=9");
				entry.ShouldContain("role=unknown");
				entry.ShouldContain("request=unknown");
				entry.ShouldNotContain("789");
			}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(5)]
		public static void TruncatedHeaderHasNoRequestCorrelation(int length)
			{
				var messages = MessageRegistry.New;
				messages.Register
					(
						new MessageLabel
							(
								TypeName: "query",
								MessageRole.Request
							)
					);
				var entries = new List<string>();
				var diagnostic = Middleware.Logging
					(
						messages,
						new CaptureLog(entries),
						context: "Truncated packet"
					);

				diagnostic.Handle(new PeerKey(7), new byte[length]);

				var entry = entries.ShouldHaveSingleItem();
				entry.ShouldContain("request=unknown");
				entry.ShouldContain($"size={length}");
			}

		private static byte[] PacketWith
			(
				MessageTypeId typeId,
				int payload
			)
			{
				var bytes = new byte[sizeof(short) + sizeof(int)];
				BinaryPrimitives.WriteInt16LittleEndian(bytes, typeId.Value);
				BinaryPrimitives.WriteInt32LittleEndian
					(
						bytes.AsSpan(sizeof(short)),
						payload
					);
				return bytes;
			}

		private sealed class CaptureLog(ICollection<string> entries): ILog
			{
				public void Write(string message)
					=> entries.Add(message);
			}
	}