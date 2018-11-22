using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Trading.Responses.Cluster
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct ClusterOrderExecuted : IClusterHftResponse
	{
		public ClusterOrderExecuted(long userId, long orderId, long executionTime, double price, double executedVolume, double unfilledVolume)
		{
			Size = MessageSize;
			TypeCode = HftResponseTypeCode.ClusterOrderExecuted;
			UserId = userId;
			OrderId = orderId;
			ExecutionTime = executionTime;
			Price = price;
			ExecutedVolume = executedVolume;
			UnfilledVolume = unfilledVolume;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref readonly ClusterOrderExecuted CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((ClusterOrderExecuted*)source)[0];
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

		public bool IsCompleted
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			// Calculations are performed on the server with much greater accuracy,
			// double type is used only as DTO, so zero is really zero.
			get => UnfilledVolume == 0;
		}

		public long UserId
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public readonly long OrderId;
		public readonly long ExecutionTime;
		public readonly double Price;
		public readonly double ExecutedVolume;
		public readonly double UnfilledVolume;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<ClusterOrderExecuted>());
	}
}