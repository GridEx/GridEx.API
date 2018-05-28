using GridEx.API.Requests;
using GridEx.API.Responses;
using System;
using System.Buffers;
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
			_socket = new Socket(SocketType.Stream, ProtocolType.Tcp)
			{
				NoDelay = true,
				Blocking = true,
				ReceiveBufferSize = 2 << 20
			};

			_receiveResponsesThread = new Thread(ReceiveResponsesLoop)
			{
				Priority = ThreadPriority.Highest
			};
		}

		public void Connect(IPEndPoint endPoint)
		{
			if (endPoint == null)
			{
				throw new ArgumentNullException(nameof(endPoint));
			}

			_socket.Connect(endPoint);

			Thread.Sleep(TimeOutAfterConnect);

			OnConnected(this);
		}

		public bool IsConnected
		{
			get { return _socket.Connected; }
		}

		public void Send<TRequest>(TRequest request) where TRequest : struct, IHftRequest
		{
			var requestBuffer = _byteArrayPool.Rent(request.Size);
			try
			{
				var requestSize = request.CopyTo(requestBuffer);
				_socket.Send(requestBuffer, 0, requestSize, SocketFlags.None);
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
			_cancellationToken = cancellationToken;

			_receiveResponsesThread.Start();

			_cancellationToken.WaitHandle.WaitOne();
		}

		public void Disconnect()
		{
			_socket.Close();

			OnDisconnected(this);
		}

		public void Dispose()
		{
			if (Interlocked.CompareExchange(ref _isDisposed, 1, 0) == 0)
			{
				_socket.Dispose();
			}
		}

		private void ReceiveResponsesLoop()
		{
			var inputBuffer = _byteArrayPool.Rent(MTUSize);
			var assemblyBuffer = _byteArrayPool.Rent(ResponseSize.Max);
			var assemblyBufferShift = 0;
			var responseSize = 0;
			try
			{
				while (!_cancellationToken.IsCancellationRequested)
				{
					var responseBytesReceived = _socket.Receive(inputBuffer, SocketFlags.None);
					var inputBufferShift = 0;
					var inputBufferTail = 0;
					do
					{
						inputBufferTail = responseBytesReceived - inputBufferShift;
						if (inputBufferTail <= 0)
						{
							break;
						}

						if (assemblyBufferShift == 0)
						{
							responseSize = inputBuffer[inputBufferShift];
						}
						else
						{
							var responseTail = assemblyBufferShift + inputBufferTail;
							if (responseTail >= responseSize)
							{
								var rest = responseSize - assemblyBufferShift;
								Buffer.BlockCopy(inputBuffer, inputBufferShift, assemblyBuffer, assemblyBufferShift, rest);
								assemblyBufferShift = 0;
								inputBufferShift += rest;
								CreateResponse(assemblyBuffer, 0);
								continue;
							}
							else
							{
								Buffer.BlockCopy(inputBuffer, inputBufferShift, assemblyBuffer, assemblyBufferShift, inputBufferTail);
								assemblyBufferShift += inputBufferTail;
								break;
							}
						}

						if (inputBufferTail >= responseSize)
						{
							CreateResponse(inputBuffer, inputBufferShift);
							inputBufferShift += responseSize;
						}
						else if (inputBufferTail > 0)
						{
							Buffer.BlockCopy(inputBuffer, inputBufferShift, assemblyBuffer, assemblyBufferShift, inputBufferTail);
							assemblyBufferShift += inputBufferTail;
							break;
						}
					} while (true);
				}
			}
			catch(SocketException socketException)
			{
				OnError(this, socketException.SocketErrorCode);
				OnDisconnected(this);
			}
			catch (Exception exception)
			{
				OnException(this, exception);
			}
			finally
			{
				_byteArrayPool.Return(assemblyBuffer);
				_byteArrayPool.Return(inputBuffer);
			}
		}
		private void CreateResponse(byte[] buffer, int offset)
		{
			switch ((ResponseTypeCode)buffer[offset + 1])
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
				case ResponseTypeCode.MarketInfo:
					ref readonly MarketInfo marketInfo = ref MarketInfo.CopyFrom(buffer, offset);
					OnMarketInfo(this, marketInfo);
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

		private int _isDisposed = 0;
		private readonly Socket _socket;
		private readonly ArrayPool<byte> _byteArrayPool = ArrayPool<byte>.Shared;
		private readonly Thread _receiveResponsesThread;
		private CancellationToken _cancellationToken;
		private static readonly TimeSpan TimeOutAfterConnect = TimeSpan.FromSeconds(1);
		private const int MTUSize = 1500;
	}
}