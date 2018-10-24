using System;
using System.Net.Sockets;
using GridEx.API.Trading.Requests;
using GridEx.API.Trading.Responses;
using GridEx.API.Trading.Responses.Cluster;
using GridEx.API.Trading.Responses.Status;
using GridEx.API.Trading.Responses.Status.Cluster;

namespace GridEx.API.Trading
{
	public delegate void OnStatusDelegate(HftSocket socket, ref CurrentStatus status);

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
					ref readonly OrderCreated orderCreated = ref OrderCreated.CopyFrom(buffer, offset);
					OnOrderCreated?.Invoke(this, orderCreated);
					break;
				case HftResponseTypeCode.OrderExecuted:
					ref readonly OrderExecuted orderExecuted = ref OrderExecuted.CopyFrom(buffer, offset);
					OnOrderExecuted?.Invoke(this, orderExecuted);
					break;
				case HftResponseTypeCode.OrderCanceled:
					ref readonly OrderCanceled orderCanceled = ref OrderCanceled.CopyFrom(buffer, offset);
					OnOrderCanceled?.Invoke(this, orderCanceled);
					break;
				case HftResponseTypeCode.AllOrdersCanceled:
					ref readonly AllOrdersCanceled allOrdersCanceled = ref AllOrdersCanceled.CopyFrom(buffer, offset);
					OnAllOrdersCanceled?.Invoke(this, allOrdersCanceled);
					break;
				case HftResponseTypeCode.AccessTokenRejected:
					ref readonly AccessTokenRejected rejectedToken = ref AccessTokenRejected.CopyFrom(buffer, offset);
					OnAccessTokenRejected?.Invoke(this, rejectedToken);
					break;
				case HftResponseTypeCode.AccessTokenAccepted:
					ref readonly AccessTokenAccepted acceptedToken = ref AccessTokenAccepted.CopyFrom(buffer, offset);
					OnAccessTokenAccepted?.Invoke(this, acceptedToken);
					break;
				case HftResponseTypeCode.OrderRejected:
					ref readonly OrderRejected orderRejected = ref OrderRejected.CopyFrom(buffer, offset);
					OnOrderRejected?.Invoke(this, orderRejected);
					break;
				case HftResponseTypeCode.RestrictionsViolated:
					ref readonly HftRestrictionsViolated restrictionsViolated = ref HftRestrictionsViolated.CopyFrom(buffer, offset);
					OnRestrictionsViolated?.Invoke(this, restrictionsViolated);
					break;
				case HftResponseTypeCode.RequestRejected:
					ref readonly HftRequestRejected requestRejected = ref HftRequestRejected.CopyFrom(buffer, offset);
					OnRequestRejected?.Invoke(this, requestRejected);
					break;
				case HftResponseTypeCode.Status:
					ref CurrentStatus currentStatus = ref CurrentStatus.CopyFrom(buffer, offset);
					OnStatus?.Invoke(this, ref currentStatus);
					break;

				case HftResponseTypeCode.ClusterOrderCreated:
					ref readonly ClusterOrderCreated clusterOrderCreated = ref ClusterOrderCreated.CopyFrom(buffer, offset);
					OnClusterOrderCreated?.Invoke(this, clusterOrderCreated);
					break;
				case HftResponseTypeCode.ClusterOrderExecuted:
					ref readonly ClusterOrderExecuted clusterOrderExecuted = ref ClusterOrderExecuted.CopyFrom(buffer, offset);
					OnClusterOrderExecuted?.Invoke(this, clusterOrderExecuted);
					break;
				case HftResponseTypeCode.ClusterOrderCanceled:
					ref readonly ClusterOrderCanceled clusterOrderCanceled = ref ClusterOrderCanceled.CopyFrom(buffer, offset);
					OnClusterOrderCanceled?.Invoke(this, clusterOrderCanceled);
					break;
				case HftResponseTypeCode.ClusterAllOrdersCanceled:
					ref readonly ClusterAllOrdersCanceled clusterAllOrdersCanceled = ref ClusterAllOrdersCanceled.CopyFrom(buffer, offset);
					OnClusterAllOrdersCanceled?.Invoke(this, clusterAllOrdersCanceled);
					break;
				case HftResponseTypeCode.ClusterOrderRejected:
					ref readonly ClusterOrderRejected clusterOrderRejected = ref ClusterOrderRejected.CopyFrom(buffer, offset);
					OnClusterOrderRejected?.Invoke(this, clusterOrderRejected);
					break;
				case HftResponseTypeCode.ClusterUserStatus:
					ref ClusterUserCurrentStatus clusterUserStatus = ref ClusterUserCurrentStatus.CopyFrom(buffer, offset);
					OnClusterStatus?.Invoke(this, ref clusterUserStatus);
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