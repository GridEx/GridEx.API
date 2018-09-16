using System;
using System.Net.Sockets;
using GridEx.API.Trading.Requests;
using GridEx.API.Trading.Responses;
using GridEx.API.Trading.Responses.Status;

namespace GridEx.API.Trading
{
	public delegate void OnCurrentStatusDelegate(HftSocket socket, ref CurrentStatus status);

	public sealed class HftSocket : GridExSocketBase
	{
		public Action<HftSocket, AccessTokenAccepted> OnAccessTokenAccepted = delegate { };

		public Action<HftSocket, AccessTokenRejected> OnAccessTokenRejected = delegate { };

		public Action<HftSocket, OrderCreated> OnOrderCreated = delegate { };

		public Action<HftSocket, OrderRejected> OnOrderRejected = delegate { };

		public Action<HftSocket, OrderExecuted> OnOrderExecuted = delegate { };

		public Action<HftSocket, OrderCanceled> OnOrderCanceled = delegate { };

		public Action<HftSocket, AllOrdersCanceled> OnAllOrdersCanceled = delegate { };

		public Action<HftSocket, HftRestrictionsViolated> OnRestrictionsViolated = delegate { };

		public Action<HftSocket, HftRequestRejected> OnRequestRejected = delegate { };

		public OnCurrentStatusDelegate OnCurrentStatus = delegate { };

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
				OnException(this, exception);
			}
		}

		protected override void CreateResponse(byte[] buffer, int offset)
		{
			switch ((HftResponseTypeCode)buffer[offset + 2])
			{
				case HftResponseTypeCode.OrderCreated:
					ref readonly OrderCreated orderCreated = ref OrderCreated.CopyFrom(buffer, offset);
					OnOrderCreated(this, orderCreated);
					break;
				case HftResponseTypeCode.OrderExecuted:
					ref readonly OrderExecuted orderExecuted = ref OrderExecuted.CopyFrom(buffer, offset);
					OnOrderExecuted(this, orderExecuted);
					break;
				case HftResponseTypeCode.OrderCanceled:
					ref readonly OrderCanceled orderCanceled = ref OrderCanceled.CopyFrom(buffer, offset);
					OnOrderCanceled(this, orderCanceled);
					break;
				case HftResponseTypeCode.AllOrdersCanceled:
					ref readonly AllOrdersCanceled allOrdersCanceled = ref AllOrdersCanceled.CopyFrom(buffer, offset);
					OnAllOrdersCanceled(this, allOrdersCanceled);
					break;
				case HftResponseTypeCode.AccessTokenRejected:
					ref readonly AccessTokenRejected rejectedToken = ref AccessTokenRejected.CopyFrom(buffer, offset);
					OnAccessTokenRejected(this, rejectedToken);
					break;
				case HftResponseTypeCode.AccessTokenAccepted:
					ref readonly AccessTokenAccepted acceptedToken = ref AccessTokenAccepted.CopyFrom(buffer, offset);
					OnAccessTokenAccepted(this, acceptedToken);
					break;
				case HftResponseTypeCode.OrderRejected:
					ref readonly OrderRejected orderRejected = ref OrderRejected.CopyFrom(buffer, offset);
					OnOrderRejected(this, orderRejected);
					break;
				case HftResponseTypeCode.RestrictionsViolated:
					ref readonly HftRestrictionsViolated restrictionsViolated = ref HftRestrictionsViolated.CopyFrom(buffer, offset);
					OnRestrictionsViolated(this, restrictionsViolated);
					break;
				case HftResponseTypeCode.RequestRejected:
					ref readonly HftRequestRejected requestRejected = ref HftRequestRejected.CopyFrom(buffer, offset);
					OnRequestRejected(this, requestRejected);
					break;
				case HftResponseTypeCode.CurrentStatus:
					ref CurrentStatus currentStatus = ref CurrentStatus.CopyFrom(buffer, offset);
					OnCurrentStatus(this, ref currentStatus);
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