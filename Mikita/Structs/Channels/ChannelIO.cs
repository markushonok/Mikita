using System;
using System.Threading.Channels;

namespace Mikita.Structs.Channels;

public static class ChannelIO
	{
		public static void Write<T>
			(
				this Channel<T> channel,
				T item
			)
			=> channel.Writer.Write(item);

		public static void Write<T>
			(
				this ChannelWriter<T> writer,
				T item
			)
			{
				if (!writer.TryWrite(item))
					throw WriteException;
			}

		public static InvalidOperationException WriteException
			=> new("Channel write operation failed.");
	}