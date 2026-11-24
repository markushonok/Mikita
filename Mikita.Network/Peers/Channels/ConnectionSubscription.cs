using LiteNetLib;
using Mikita.Observation.Subscriptions;
using static LiteNetLib.EventBasedNetListener;

namespace Mikita.Network.Peers.Channels;

/// <summary>
/// Subscribes to LiteNetLib peer connect
/// and disconnect events as a deactivatable
/// <see cref="ISubscription"/>.
/// </summary>
public static class ConnectionSubscription
	{
		extension(EventBasedNetListener listener)
			{
				public ISubscription SubscriptionOf
					(
						OnPeerConnected connected,
						OnPeerDisconnected disconnected
					)
					=> new Subscription(listener, connected, disconnected);
			}

		private sealed class Subscription
			(
				EventBasedNetListener listener,
				OnPeerConnected connected,
				OnPeerDisconnected disconnected
			)
			: ISubscription
			{

				public void Activate()
					{
						listener.PeerConnectedEvent += connected;
						listener.PeerDisconnectedEvent += disconnected;
					}

				public void Deactivate()
					{
						listener.PeerConnectedEvent -= connected;
						listener.PeerDisconnectedEvent -= disconnected;
					}
			}
	}