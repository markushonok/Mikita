using Mikita.Nulls;
using Mikita.Routines.Assignment;
using Mikita.Threading;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Structs.Referring;

public static class RefAssignment
	{
		extension<T>(IRef<T> reference)
			{
				public Assignment<T> Assignment
					=> new(value => reference.Value = value);

				public async Task AsyncSetTo
					(
						Task<T> value
					)
					=> reference.Value = await value;

				public void SetTo
					(
						T value
					)
					=> reference.Value = value;

				public void SetTo
					(
						T value,
						CancellationToken cancel
					)
					{
						cancel.ThrowIfRequested();
						reference.Value = value;
					}
			}

		extension<T>(IRef<T?> reference)
			{
				public void TryPullOut(Action<T> action)
					{
						if (reference.Value is not null)
							reference.PullOut(action);
					}

				public void PullOut(Action<T> action)
					{
						action(reference.Value.NotNull());
						reference.SetTo(default);
					}

				public void PullOut(out T? value)
					{
						value = reference.Value;
						reference.SetTo(default);
					}
			}
	}