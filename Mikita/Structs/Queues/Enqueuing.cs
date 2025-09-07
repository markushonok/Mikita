using System;
using System.Collections.Generic;

namespace Mikita.Structs.Queues;

public static class Enqueuing
	{
		public static IEnumerable<T> DequeueWhile<T>
			(
				this Queue<T> queue,
				Func<T, bool> predicate
			)
			{
				while (queue.Count > 0 && predicate(queue.Peek()))
					yield return queue.Dequeue();
			}

		public static IEnumerable<T> DequeueMany<T>
			(
				this Queue<T> queue,
				int count
			)
			{
				for (var i = 0; i < count; i++)
					yield return queue.Dequeue();
			}

		public static IEnumerable<T> DequeueUntil<T>
			(
				this Queue<T> queue,
				Func<T, bool> predicate
			)
			{
				while (queue.Count > 0 && !predicate(queue.Peek()))
					yield return queue.Dequeue();
			}
	}