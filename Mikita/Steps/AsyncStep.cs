using System;
using System.Threading.Tasks;

namespace Mikita.Steps;

public sealed class AsyncStep
	(
		Func<Task> @do,
		Func<Task> undo
	)
	: IAsyncStep
	{
		public Task Do()
			=> @do();

		public Task Undo()
			=> undo();
	}