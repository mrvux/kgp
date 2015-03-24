//Converts back uint buffer to float version

struct BoundingBox
{
	float3 boundsMin;
	float3 boundsMax;
};

StructuredBuffer<uint3> BoundsMinBuffer : register(t0);
StructuredBuffer<uint3> BoundsMaxBuffer : register(t1);

RWStructuredBuffer<BoundingBox> RWBoundsBuffer : register(u0);

//From bullet physics library
float3 UintToSignedFloat(uint3 b)
{
	b.x ^= (((b.x >> 31) - 1) | 0x80000000);
	b.y ^= (((b.y >> 31) - 1) | 0x80000000);
	b.z ^= (((b.z >> 31) - 1) | 0x80000000);
	return float3(asfloat(b.x),asfloat(b.y),asfloat(b.z));
}

BoundingBox GetBoundingBox(uint id)
{
	uint3 bmin = BoundsMinBuffer[id];
	uint3 bmax = BoundsMaxBuffer[id];

	BoundingBox box;
	box.boundsMin = UintToSignedFloat(bmin);
	box.boundsMax = UintToSignedFloat(bmax);
	return box;
}

[numthreads(1,1,1)]
void CS(uint3 tid : SV_DispatchThreadID)
{
	RWBoundsBuffer[tid.x] = GetBoundingBox(tid.x);
}


