﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Trading.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct AccessTokenRejected : IHftResponse
	{
		public AccessTokenRejected(ApiVersion apiVersion, HftRejectReasonCode rejectCode)
		{
			Size = MessageSize;
			TypeCode = HftResponseTypeCode.AccessTokenRejected;
			ApiVersion = apiVersion;
			RejectCode = rejectCode;
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref readonly AccessTokenRejected CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((AccessTokenRejected*)source)[0];
			}
		}

		public ushort Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public HftResponseTypeCode TypeCode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public readonly ApiVersion ApiVersion;

		public readonly HftRejectReasonCode RejectCode;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<AccessTokenRejected>());
	}
}
