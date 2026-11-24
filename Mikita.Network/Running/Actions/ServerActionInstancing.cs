using LiteNetLib;
using Mikita.Structs.Referring;
using System;
using System.Diagnostics;
using System.Threading;

namespace Mikita.Network.Running.Actions;

public static class ServerActionInstancing
	{
		extension(PeerActionRun)
			{
				public static PeerActionRun NewWith
					(
						NetManager netManager
					)
					=> PeerActionRun.NewWith
						(
							loop: isRunning => ServerActionLoop.Start
								(
									isRunning,
									netManager
								)
						);

				public static PeerActionRun NewWith
					(
						ServerActionLoop loop
					)
					=> PeerActionRun.NewWith
						(
							loop,
							isRunning: Ref.To(false),
							Ref<Thread>.Null
						);

				public static PeerActionRun NewWith
					(
						ServerActionLoop loop,
						IRef<bool> isRunning,
						IRef<Thread?> thread
					)
					=> new
						(
							action: () => loop(isRunning.AsFunction),
							isRunning,
							thread
						);
			}

		extension(ServerActionLoop)
			{
				public static void Start
					(
						Func<bool> isRunning,
						NetManager netManager
					)
					{
						var stopwatch = new Stopwatch();

						while (isRunning())
							{
								stopwatch.Restart();
								netManager.PollEvents();

								var elapsed = (int) stopwatch.ElapsedMilliseconds;
								var sleepTime = 100 - elapsed;

								if (sleepTime > 0)
									{
										Thread.Sleep(sleepTime);
									}
							}
					}
			}
	}