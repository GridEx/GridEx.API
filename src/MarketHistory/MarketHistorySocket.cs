using System;
using System.Net.Sockets;
using GridEx.API.MarketHistory.Requests;
using GridEx.API.MarketHistory.Responses;

namespace GridEx.API.MarketHistory
{
	public delegate void OnLastHistoryDelegate(MarketHistorySocket socket, ref LastHistory history);

	public delegate void OnHistoryDelegate(MarketHistorySocket socket, ref History history);

	public sealed class MarketHistorySocket : GridExSocketBase
	{
		public event Action<MarketHistorySocket, TickChange> OnTickChange;

		public event Action<MarketHistorySocket, HistoryRequestRejected> OnRequestRejected;

		public event Action<MarketHistorySocket, HistoryStatus> OnHistoryStatus;

		public event OnHistoryDelegate OnHistory;

		public event OnLastHistoryDelegate OnLastHistory;

		public event Action<MarketHistorySocket, HistoryRestrictionsViolated> OnRestrictionsViolated;

		public event Action<MarketHistorySocket, HistorySettings> OnSettings;

		public MarketHistorySocket()
			: base(HistoryResponseSize.Max)
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
				RaiseOnException(exception);
			}
		}

		protected override int MaxResponseSize => HistoryResponseSize.Max;

		protected override void CreateResponse(byte[] buffer, int offset)
		{

			switch ((HistoryResponseTypeCode)buffer[offset + 2])
			{
				case HistoryResponseTypeCode.TickChange:
					ref readonly var tickChange = ref TickChange.CopyFrom(buffer, offset);
					OnTickChange?.Invoke(this, tickChange);
					break;
				case HistoryResponseTypeCode.HistoryStatus:
					ref readonly var historyStatus = ref HistoryStatus.CopyFrom(buffer, offset);
					OnHistoryStatus?.Invoke(this, historyStatus);
					break;
				case HistoryResponseTypeCode.History:
					ref var history = ref History.CopyFrom(buffer, offset);
					OnHistory?.Invoke(this, ref history);
					break;
				case HistoryResponseTypeCode.LastHistory:
					ref var lastHistory = ref LastHistory.CopyFrom(buffer, offset);
					OnLastHistory?.Invoke(this, ref lastHistory);
					break;
				case HistoryResponseTypeCode.RequestRejected:
					ref readonly var requestRejected = ref HistoryRequestRejected.CopyFrom(buffer, offset);
					OnRequestRejected?.Invoke(this, requestRejected);
					break;
				case HistoryResponseTypeCode.RestrictionsViolated:
					ref readonly var restrictionsViolated = ref HistoryRestrictionsViolated.CopyFrom(buffer, offset);
					OnRestrictionsViolated?.Invoke(this, restrictionsViolated);
					break;
				case HistoryResponseTypeCode.HistorySettings:
					ref var settings = ref HistorySettings.CopyFrom(buffer, offset);
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