﻿using GridEx.API.Trading.Responses;

namespace GridEx.API.Trading.Requests
{
	public interface IHftRequest : ISizeable
    {
		HftRequestTypeCode TypeCode { get; }

		long RequestId { get; }

		int CopyTo(byte[] buffer, int offset = 0);

		HftRejectReasonCode IsValid();
	}
}