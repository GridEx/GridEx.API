using System;
using System.Net.Sockets;
using GridEx.API.Trading.Requests;
using GridEx.API.Trading.Responses;
using GridEx.API.Trading.Responses.Cluster;
using GridEx.API.Trading.Responses.Status;
using GridEx.API.Trading.Responses.Status.Cluster;
using AccessTokenAccepted = GridEx.API.Trading.Responses.AccessTokenAccepted;
using AccessTokenRejected = GridEx.API.Trading.Responses.AccessTokenRejected;

namespace GridEx.API.Trading
{
	public delegate void OnStatusDelegate(HftSocket socket, ref UserCurrentStatus status);

	public delegate void OnClusterUserStatusDelegate(HftSocket socket, ref ClusterUserCurrentStatus status);

	public sealed class HftSocket : GridExSocketBase
	{
		public event Action<HftSocket, AccessTokenAccepted> OnAccessTokenAccepted;

		public event Action<HftSocket, AccessTokenRejected> OnAccessTokenRejected;

		public event Action<HftSocket, OrderCreated> OnOrderCreated;

		public event Action<HftSocket, OrderRejected> OnOrderRejected;

		public event Action<HftSocket, OrderExecuted> OnOrderExecuted;

		public event Action<HftSocket, OrderCanceled> OnOrderCanceled;

		public event Action<HftSocket, AllOrdersCanceled> OnAllOrdersCanceled;

		public event Action<HftSocket, HftRestrictionsViolated> OnRestrictionsViolated;

		public event Action<HftSocket, HftRequestRejected> OnRequestRejected;

		public event OnStatusDelegate OnStatus;

		public event Action<HftSocket, ClusterOrderCreated> OnClusterOrderCreated;

		public event Action<HftSocket, ClusterOrderRejected> OnClusterOrderRejected;

		public event Action<HftSocket, ClusterOrderExecuted> OnClusterOrderExecuted;

		public event Action<HftSocket, ClusterOrderCanceled> OnClusterOrderCanceled;

		public event Action<HftSocket, ClusterAllOrdersCanceled> OnClusterAllOrdersCanceled;

		public event OnClusterUserStatusDelegate OnClusterStatus;

		public event Action<HftSocket, HftMarketSettings> OnSettings;

		public HftSocket()
			: base(HftResponseSize.Max)
		{
			_requestBuffer = new byte[HftRequestSize.Max];
		}

		//Don't call from different threads at the same time, because the allocated buffer is used for all calls
		public void Send<TRequest>(TRequest request) where TRequest : struct, IHftRequest
		{
			try
			{
				var requestSize = request.CopyTo(_requestBuffer);
				_socket.Send(_requestBuffer, requestSize, SocketFlags.None);
			}
			catch (Exception exception)
			{
				RaiseOnException(exception);
			}
		}

		protected override void CreateResponse(byte[] buffer, int offset)
		{
			switch ((HftResponseTypeCode)buffer[offset + 2])
			{
				case HftResponseTypeCode.OrderCreated:
					ref readonly var orderCreated = ref OrderCreated.CopyFrom(buffer, offset);
					OnOrderCreated?.Invoke(this, orderCreated);
					break;
				case HftResponseTypeCode.OrderExecuted:
					ref readonly var orderExecuted = ref OrderExecuted.CopyFrom(buffer, offset);
					OnOrderExecuted?.Invoke(this, orderExecuted);
					break;
				case HftResponseTypeCode.OrderCanceled:
					ref readonly var orderCanceled = ref OrderCanceled.CopyFrom(buffer, offset);
					OnOrderCanceled?.Invoke(this, orderCanceled);
					break;
				case HftResponseTypeCode.AllOrdersCanceled:
					ref readonly var allOrdersCanceled = ref AllOrdersCanceled.CopyFrom(buffer, offset);
					OnAllOrdersCanceled?.Invoke(this, allOrdersCanceled);
					break;
				case HftResponseTypeCode.AccessTokenRejected:
					ref readonly var rejectedToken = ref AccessTokenRejected.CopyFrom(buffer, offset);
					OnAccessTokenRejected?.Invoke(this, rejectedToken);
					break;
				case HftResponseTypeCode.AccessTokenAccepted:
					ref readonly var acceptedToken = ref AccessTokenAccepted.CopyFrom(buffer, offset);
					OnAccessTokenAccepted?.Invoke(this, acceptedToken);
					break;
				case HftResponseTypeCode.OrderRejected:
					ref readonly var orderRejected = ref OrderRejected.CopyFrom(buffer, offset);
					OnOrderRejected?.Invoke(this, orderRejected);
					break;
				case HftResponseTypeCode.RestrictionsViolated:
					ref readonly var restrictionsViolated = ref HftRestrictionsViolated.CopyFrom(buffer, offset);
					OnRestrictionsViolated?.Invoke(this, restrictionsViolated);
					break;
				case HftResponseTypeCode.RequestRejected:
					ref readonly var requestRejected = ref HftRequestRejected.CopyFrom(buffer, offset);
					OnRequestRejected?.Invoke(this, requestRejected);
					break;
				case HftResponseTypeCode.Status:
					ref var currentStatus = ref UserCurrentStatus.CopyFrom(buffer, offset);
					OnStatus?.Invoke(this, ref currentStatus);
					break;

				case HftResponseTypeCode.ClusterOrderCreated:
					ref readonly var clusterOrderCreated = ref ClusterOrderCreated.CopyFrom(buffer, offset);
					OnClusterOrderCreated?.Invoke(this, clusterOrderCreated);
					break;
				case HftResponseTypeCode.ClusterOrderExecuted:
					ref readonly var clusterOrderExecuted = ref ClusterOrderExecuted.CopyFrom(buffer, offset);
					OnClusterOrderExecuted?.Invoke(this, clusterOrderExecuted);
					break;
				case HftResponseTypeCode.ClusterOrderCanceled:
					ref readonly var clusterOrderCanceled = ref ClusterOrderCanceled.CopyFrom(buffer, offset);
					OnClusterOrderCanceled?.Invoke(this, clusterOrderCanceled);
					break;
				case HftResponseTypeCode.ClusterAllOrdersCanceled:
					ref readonly var clusterAllOrdersCanceled = ref ClusterAllOrdersCanceled.CopyFrom(buffer, offset);
					OnClusterAllOrdersCanceled?.Invoke(this, clusterAllOrdersCanceled);
					break;
				case HftResponseTypeCode.ClusterOrderRejected:
					ref readonly var clusterOrderRejected = ref ClusterOrderRejected.CopyFrom(buffer, offset);
					OnClusterOrderRejected?.Invoke(this, clusterOrderRejected);
					break;
				case HftResponseTypeCode.ClusterUserStatus:
					ref var clusterUserStatus = ref ClusterUserCurrentStatus.CopyFrom(buffer, offset);
					OnClusterStatus?.Invoke(this, ref clusterUserStatus);
					break;
				
				case HftResponseTypeCode.MarketSettings:
					ref var settings = ref HftMarketSettings.CopyFrom(buffer, offset);
					OnSettings?.Invoke(this, settings);
					break;

				default:
					;
					// unknown response
					break;
			}
		}

		private readonly byte[] _requestBuffer;
	}
}