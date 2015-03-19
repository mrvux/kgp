//Converts back uint buffer to float version

StructuredBuffer<uint3> BoundsBuffer : register(t0);
RWStructuredBuffer<float3> RWFloatBuffer :register(u0);

[numthreads(1,1,1)]
void CS_ToFloat(uint3 tid : SV_DispatchThreadID)
{
	uint3 b = BoundsBuffer[tid.x];
	
	b.x ^= (((b.x >> 31) - 1) | 0x80000000);
	b.y ^= (((b.y >> 31) - 1) | 0x80000000);
	b.z ^= (((b.z >> 31) - 1) | 0x80000000);
	
	RWFloatBuffer[tid.x] = float3(asfloat(b.x),asfloat(b.y),asfloat(b.z));
}



