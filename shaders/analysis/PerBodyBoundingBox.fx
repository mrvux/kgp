//Constructs a bounding box for a point set, one bounding box per body index

ByteAddressBuffer ElementCountBuffer : register(t0);

StructuredBuffer<float3> PositionBuffer : register(t1);
StructuredBuffer<uint> BodyIndexBuffer : register(t2);

RWStructuredBuffer<uint3> RWBoundsMinBuffer : register (u0);
RWStructuredBuffer<uint3> RWBoundsMaxBuffer : register (u0);

//From bullet physics, turn a signed float as sortable uint
uint3 toSignUint(float3 d)
{
	uint3 xyz = uint3(asuint(d.x),asuint(d.y), asuint(d.z));	
	xyz.x ^= (1+~(xyz.x >> 31) | 0x80000000);
	xyz.y ^= (1+~(xyz.y >> 31) | 0x80000000);		
	xyz.z ^= (1+~(xyz.z >> 31) | 0x80000000);
}

[numthreads(64, 1, 1)]
void CS_ProcessSimple( uint3 DTid : SV_DispatchThreadID )
{
	uint tid = DTid.x;
	uint elementCount = ElementCountBuffer.Load(0);
	if (tid >= elementCount)
		return;
		
	float3 p = PositionBuffer[tid];
	uint3 xyz = toSignUint(p);	
	uint pid = BodyIndexBuffer[tid];
	
	InterlockedMin(RWBoundsMinBuffer[pid].x, xyz.x);
	InterlockedMin(RWBoundsMinBuffer[pid].y, xyz.y);
	InterlockedMin(RWBoundsMinBuffer[pid].z, xyz.z);
		
	InterlockedMax(RWBoundsMaxBuffer[pid].x, xyz.x);
	InterlockedMax(RWBoundsMaxBuffer[pid].y, xyz.y);
	InterlockedMax(RWBoundsMaxBuffer[pid].z, xyz.z);
}
