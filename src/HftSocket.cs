﻿using GridEx.API.Requests;
using GridEx.API.Responses;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;

namespace GridEx.API
{
	public sealed class HftSocket : IDisposable
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

		public Action<HftSocket, Exception> OnException = delegate { };

		public Action<HftSocket> OnConnected = delegate { };

		public Action<HftSocket, SocketError> OnError = delegate { };

		public Action<HftSocket> OnDisconnected = delegate { };

		public HftSocket()
		{
			if (!BitConverter.IsLittleEndian)
			{
				throw new NotSupportedException("Little endian byte ordering is only supported.");
			}

			_socket = new Socket(SocketType.Stream, ProtocolType.Tcp)
			{
				NoDelay = true,
				Blocking = true,
				ReceiveBufferSize = 2 << 20
			};

			_requestBuffer = new byte[RequestSize.Max];

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

		public void WaitResponses(CancellationToken cancellationToken)
		{
			_cancellationToken = cancellationToken;

			_receiveResponsesThread.Start();

			_cancellationToken.WaitHandle.WaitOne();
		}

		public void Disconnect()
		{
			_socket.Disconnect(false);

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
			var inputBuffer = new byte[MTUSize];
			var assemblyBuffer = new byte[ResponseSize.Max];
			var assemblyBufferShift = 0;
			var responseSize = 0;
			byte partOfResponseSize = 0;
			try
			{
				while (!_cancellationToken.IsCancellationRequested)
				{
					var responseBytesReceived = _socket.Receive(inputBuffer, SocketFlags.None);
					var inputBufferShift = 0;
					do
					{
						var inputBufferTail = responseBytesReceived - inputBufferShift;
						if (inputBufferTail <= 0)
						{
							break;
						}

						if (partOfResponseSize != 0)
						{
							responseSize = inputBuffer[inputBufferShift] << 8 | partOfResponseSize;
							partOfResponseSize = 0;

							if (ProcessResponseTail(
								inputBuffer,  
								assemblyBuffer, 
								responseSize, 
								inputBufferTail, 
								ref inputBufferShift, 
								ref assemblyBufferShift))
							{
								continue;
							}

							break;
						}

						if (assemblyBufferShift == 0)
						{
							if (inputBufferTail >= 2)
							{
								responseSize = BitConverter.ToUInt16(inputBuffer, inputBufferShift);
							}
							else
							{
								partOfResponseSize = inputBuffer[inputBufferShift];
								responseSize = int.MaxValue;
							}
						}
						else
						{
							if (ProcessResponseTail(inputBuffer, assemblyBuffer, responseSize, inputBufferTail, ref inputBufferShift, ref assemblyBufferShift))
							{
								continue;
							}

							break;
						}

						if (inputBufferTail >= responseSize)
						{
							CreateResponse(inputBuffer, inputBufferShift);

							inputBufferShift += responseSize;
						}
						else
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
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool ProcessResponseTail(
			byte[] inputBuffer,
			byte[] assemblyBuffer,
			int responseSize,
			int inputBufferTail,
			ref int inputBufferShift,
			ref int assemblyBufferShift)
		{
			var responseTail = assemblyBufferShift + inputBufferTail;
			if (responseTail >= responseSize)
			{
				var rest = responseSize - assemblyBufferShift;
				Buffer.BlockCopy(inputBuffer, inputBufferShift, assemblyBuffer, assemblyBufferShift, rest);
				assemblyBufferShift = 0;
				inputBufferShift += rest;

				CreateResponse(assemblyBuffer, 0);
				return true;
			}

			Buffer.BlockCopy(inputBuffer, inputBufferShift, assemblyBuffer, assemblyBufferShift, inputBufferTail);
			assemblyBufferShift += inputBufferTail;
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CreateResponse(byte[] buffer, int offset)
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

		private int _isDisposed = 0;
		private readonly Socket _socket;
		private readonly Thread _receiveResponsesThread;
		private CancellationToken _cancellationToken;
		private static readonly TimeSpan TimeOutAfterConnect = TimeSpan.FromSeconds(1);
		private const int MTUSize = 1500;
		private readonly byte[] _requestBuffer;
	}
}