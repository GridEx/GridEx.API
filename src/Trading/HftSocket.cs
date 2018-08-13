using System;
using System.Net.Sockets;
using GridEx.API.Trading.Requests;
using GridEx.API.Trading.Responses;

namespace GridEx.API.Trading
{
	public sealed class HftSocket : GridExSocketBase
	{
		public Action<HftSocket, UserTokenAccepted> OnUserTokenAccepted = delegate { };

		public Action<HftSocket, UserTokenRejected> OnUserTokenRejected = delegate { };

		public Action<HftSocket, OrderCreated> OnOrderCreated = delegate { };

		public Action<HftSocket, OrderRejected> OnOrderRejected = delegate { };

		public Action<HftSocket, OrderExecuted> OnOrderExecuted = delegate { };

		public Action<HftSocket, OrderCancelled> OnOrderCancelled = delegate { };

		public Action<HftSocket, AllOrdersCancelled> OnAllOrdersCancelled = delegate { };

		public Action<HftSocket, ConnectionTooSlow> OnConnectionTooSlow = delegate { };

		public Action<HftSocket, RequestRejected> OnRequestRejected = delegate { };

		public HftSocket()
			: base(ResponseSize.Max)
		{
			_requestBuffer = new byte[RequestSize.Max];
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
			switch ((ResponseTypeCode)buffer[offset + 2])
			{
				case ResponseTypeCode.OrderCreated:
					ref readonly OrderCreated orderCreated = ref OrderCreated.CopyFrom(buffer, offset);
					OnOrderCreated(this, orderCreated);
					break;
				case ResponseTypeCode.OrderExecuted:
					ref readonly OrderExecuted orderExecuted = ref OrderExecuted.CopyFrom(buffer, offset);
					OnOrderExecuted(this, orderExecuted);
					break;
				case ResponseTypeCode.OrderCancelled:
					ref readonly OrderCancelled orderCancelled = ref OrderCancelled.CopyFrom(buffer, offset);
					OnOrderCancelled(this, orderCancelled);
					break;
				case ResponseTypeCode.AllOrdersCancelled:
					ref readonly AllOrdersCancelled allOrdersCancelled = ref AllOrdersCancelled.CopyFrom(buffer, offset);
					OnAllOrdersCancelled(this, allOrdersCancelled);
					break;
				case ResponseTypeCode.UserTokenRejected:
					ref readonly UserTokenRejected rejectedToken = ref UserTokenRejected.CopyFrom(buffer, offset);
					OnUserTokenRejected(this, rejectedToken);
					break;
				case ResponseTypeCode.UserTokenAccepted:
					ref readonly UserTokenAccepted acceptedToken = ref UserTokenAccepted.CopyFrom(buffer, offset);
					OnUserTokenAccepted(this, acceptedToken);
					break;
				case ResponseTypeCode.OrderRejected:
					ref readonly OrderRejected orderRejected = ref OrderRejected.CopyFrom(buffer, offset);
					OnOrderRejected(this, orderRejected);
					break;
				case ResponseTypeCode.ConnectionTooSlow:
					ref readonly ConnectionTooSlow connectionTooSlow = ref ConnectionTooSlow.CopyFrom(buffer, offset);
					OnConnectionTooSlow(this, connectionTooSlow);
					break;
				case ResponseTypeCode.RequestRejected:
					ref readonly RequestRejected requestRejected = ref RequestRejected.CopyFrom(buffer, offset);
					OnRequestRejected(this, requestRejected);
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