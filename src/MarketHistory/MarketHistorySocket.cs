using System;
using System.Net.Sockets;
using GridEx.API.MarketHistory.Requests;
using GridEx.API.MarketHistory.Responses;

namespace GridEx.API.MarketHistory
{
	public delegate void OnLastHistoryDelegate(MarketHistorySocket socket, ref LastHistory history);

	public sealed class MarketHistorySocket : GridExSocketBase
	{
		public Action<MarketHistorySocket, TickChange> OnTickChange = delegate { };

		public Action<MarketHistorySocket, HistoryRequestRejected> OnRequestRejected = delegate { };

		public OnLastHistoryDelegate OnLastHistory = delegate { };

		public Action<MarketHistorySocket, HistoryRestrictionsViolated> OnRestrictionsViolated = delegate { };

		public MarketHistorySocket(int maxResponseSize)
			: base(maxResponseSize)
		{
			_requestBuffer = new byte[HistoryRequestSize.Max];
		}

		//Don't call from different threads at the same time, because the allocated buffer is used for all calls
		public void Send<TRequest>(TRequest request) where TRequest : struct, IHistoryRequest
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

		protected override int MaxResponseSize => HistoryResponseSize.Max;

		protected override void CreateResponse(byte[] buffer, int offset)
		{

			switch ((HistoryResponseTypeCode)buffer[offset + 2])
			{
				case HistoryResponseTypeCode.TickChange:
					ref readonly TickChange tickChange = ref TickChange.CopyFrom(buffer, offset);
					OnTickChange(this, tickChange);
					break;
				case HistoryResponseTypeCode.LastHistory:
					ref LastHistory lastHistory = ref LastHistory.CopyFrom(buffer, offset);
					OnLastHistory(this, ref lastHistory);
					break;
				case HistoryResponseTypeCode.RequestRejected:
					ref readonly HistoryRequestRejected requestRejected = ref HistoryRequestRejected.CopyFrom(buffer, offset);
					OnRequestRejected(this, requestRejected);
					break;
				case HistoryResponseTypeCode.RestrictionsViolated:
					ref readonly HistoryRestrictionsViolated restrictionsViolated = ref HistoryRestrictionsViolated.CopyFrom(buffer, offset);
					OnRestrictionsViolated(this, restrictionsViolated);
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