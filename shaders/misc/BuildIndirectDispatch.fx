ByteAddressBuffer ElementCountBuffer : register(t0);
RWStructuredBuffer<uint> RWDispatchBuffer : register(u0);

[numthreads(1, 1, 1)]
void CS_GenerateDispatchCall( uint3 DTid : SV_DispatchThreadID )
{
	uint elementCount = ElementCountBuffer.Load(0);

	RWDispatchBuffer[0] = (elementCount + 63) / 64;
	RWDispatchBuffer[1] = 1;
	RWDispatchBuffer[2] = 1;
}

