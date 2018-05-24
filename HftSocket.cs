using GridEx.API.Requests;
using GridEx.API.Responses;
using System;
using System.Buffers;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GridEx.API
{
	public sealed class HftSocket : IDisposable
	{
		public Action<HftSocket, UserTokenAccepted> OnUserTokenAccepted = delegate { };

		public Action<HftSocket, UserTokenRejected> OnUserTokenRejected = delegate { };

		public Action<HftSocket, MarketInfo> OnMarketInfo = delegate { };

		public Action<HftSocket, OrderCreated> OnOrderCreated = delegate { };

		public Action<HftSocket, OrderRejected> OnOrderRejected = delegate { };

		public Action<HftSocket, OrderExecuted> OnOrderExecuted = delegate { };

		public Action<HftSocket, OrderCancelled> OnOrderCancelled = delegate { };

		public Action<HftSocket, AllOrdersCancelled> OnAllOrdersCancelled = delegate { };

		public Action<HftSocket, ConnectionTooSlow> OnConnectionTooSlow = delegate { };

		public Action<HftSocket, RequestRejected> OnRequestRejected = delegate { };

		public Action<HftSocket, Exception> OnException = delegate { };

		public Action<HftSocket> OnConnected = delegate { };

		public Action<HftSocket, SocketError> OnError = delegate { };

		public Action<HftSocket> OnDisconnected = delegate { };

		public HftSocket()
		{
			_tcpClient = new TcpClient
			{
				NoDelay = true
			};
		}

		public void Connect(IPEndPoint endPoint)
		{
			if (endPoint == null)
			{
				throw new ArgumentNullException(nameof(endPoint));
			}

			_tcpClient.Connect(endPoint);
			_stream = _tcpClient.GetStream();
			Thread.Sleep(TimeOutAfterConnect);

			OnConnected(this);
		}

		public bool IsConnected
		{
			get { return _tcpClient.Connected; }
		}

		public void Send<TRequest>(TRequest request) where TRequest : struct, IHftRequest
		{
			if (_stream == null)
			{
				return;
			}

			var requestBuffer = _byteArrayPool.Rent(request.Size);
			try
			{
				var requestSize = request.CopyTo(requestBuffer);
				_stream.Write(requestBuffer, 0, requestSize);
			}
			catch (Exception exception)
			{
				OnException(this, exception);
			}
			finally
			{
				_byteArrayPool.Return(requestBuffer);
			}
		}

		public void WaitResponses(CancellationToken cancellationToken)
		{
			var responseBuffer = _byteArrayPool.Rent(ResponseSize.Max);
			try
			{
				while (!cancellationToken.IsCancellationRequested)
				{
					var responseBytesReceived = _stream.Read(responseBuffer, 0, ResponseSize.Min);

					if (responseBytesReceived == 0)
					{
						OnDisconnected(this);
						return;
					}

					var responseSize = responseBuffer[0];

					if (responseSize < ResponseSize.Min || ResponseSize.Max < responseSize)
					{
						throw new IOException($"Wrong response size '{responseSize}'.");
					}

					while ((responseBytesReceived += _stream.Read(
						responseBuffer,
						responseBytesReceived,
						responseSize - responseBytesReceived)) < responseSize)
					{
					}

					if (responseBytesReceived != responseSize)
					{
						throw new IOException($"Wrong message bytes amount was received: '{responseBytesReceived}'.");
					}

					CreateResponse(responseBuffer);
				}
			}
			catch (Exception exception)
			{
				OnException(this, exception);
			}
			finally
			{
				_byteArrayPool.Return(responseBuffer);
			}
		}

		public void Disconnect()
		{
			_stream.Close();
			_tcpClient.Close();

			OnDisconnected(this);
		}

		public void Dispose()
		{
			if (Interlocked.CompareExchange(ref _isDisposed, 1, 0) == 0)
			{
				_tcpClient.Dispose();
			}
		}

		private void CreateResponse(byte[] buffer)
		{
			switch ((ResponseTypeCode)buffer[1])
			{
				case ResponseTypeCode.OrderCreated:
					ref readonly OrderCreated orderCreated = ref OrderCreated.CopyFrom(buffer);
					OnOrderCreated(this, orderCreated);
					break;
				case ResponseTypeCode.OrderExecuted:
					ref readonly OrderExecuted orderExecuted = ref OrderExecuted.CopyFrom(buffer);
					OnOrderExecuted(this, orderExecuted);
					break;
				case ResponseTypeCode.OrderCancelled:
					ref readonly OrderCancelled orderCancelled = ref OrderCancelled.CopyFrom(buffer);
					OnOrderCancelled(this, orderCancelled);
					break;
				case ResponseTypeCode.AllOrdersCancelled:
					ref readonly AllOrdersCancelled allOrdersCancelled = ref AllOrdersCancelled.CopyFrom(buffer);
					OnAllOrdersCancelled(this, allOrdersCancelled);
					break;
				case ResponseTypeCode.UserTokenRejected:
					ref readonly UserTokenRejected rejectedToken = ref UserTokenRejected.CopyFrom(buffer);
					OnUserTokenRejected(this, rejectedToken);
					break;
				case ResponseTypeCode.UserTokenAccepted:
					ref readonly UserTokenAccepted acceptedToken = ref UserTokenAccepted.CopyFrom(buffer);
					OnUserTokenAccepted(this, acceptedToken);
					break;
				case ResponseTypeCode.OrderRejected:
					ref readonly OrderRejected orderRejected = ref OrderRejected.CopyFrom(buffer);
					OnOrderRejected(this, orderRejected);
					break;
				case ResponseTypeCode.MarketInfo:
					ref readonly MarketInfo marketInfo = ref MarketInfo.CopyFrom(buffer);
					OnMarketInfo(this, marketInfo);
					break;
				case ResponseTypeCode.ConnectionTooSlow:
					ref readonly ConnectionTooSlow connectionTooSlow = ref ConnectionTooSlow.CopyFrom(buffer);
					OnConnectionTooSlow(this, connectionTooSlow);
					break;
				case ResponseTypeCode.RequestRejected:
					ref readonly RequestRejected requestRejected = ref RequestRejected.CopyFrom(buffer);
					OnRequestRejected(this, requestRejected);
					break;
				default:
					;
					// unknown response
					break;
			}
		}

		private int _isDisposed = 0;
		private readonly TcpClient _tcpClient;
		private NetworkStream _stream;
		private readonly ArrayPool<byte> _byteArrayPool = ArrayPool<byte>.Shared;
		private static readonly TimeSpan TimeOutAfterConnect = TimeSpan.FromSeconds(1);
	}
}