using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;

namespace GridEx.API
{
	public abstract class GridExSocketBase
	{
		public event Action<GridExSocketBase, Exception> OnException;

		public event Action<GridExSocketBase> OnConnected;

		public event Action<GridExSocketBase, SocketError> OnError;

		public event Action<GridExSocketBase> OnDisconnected;

		protected GridExSocketBase(int maxResponseSize)
		{
			if (!BitConverter.IsLittleEndian)
			{
				throw new NotSupportedException("Little endian byte ordering is only supported.");
			}

			_maxResponseSize = maxResponseSize;

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

			OnConnected?.Invoke(this);
		}

		public bool IsConnected
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return _socket.Connected; }
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

			OnDisconnected?.Invoke(this);
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
			var inputBuffer = new byte[MaxResponseSize];
			var assemblyBuffer = new byte[_maxResponseSize];
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
			catch (SocketException socketException)
			{
				OnError?.Invoke(this, socketException.SocketErrorCode);
				OnDisconnected?.Invoke(this);
			}
			catch (Exception exception)
			{
				RaiseOnException(exception);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected void RaiseOnException(Exception exception)
		{
			OnException?.Invoke(this, exception);
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

		protected virtual int MaxResponseSize => MTUSize;

		protected abstract void CreateResponse(byte[] buffer, int offset);

		protected readonly Socket _socket;

		private int _isDisposed;
		private readonly Thread _receiveResponsesThread;
		private CancellationToken _cancellationToken;
		private static readonly TimeSpan TimeOutAfterConnect = TimeSpan.FromSeconds(1);
		private readonly int _maxResponseSize;
		private const int MTUSize = 1472;
	}
}
