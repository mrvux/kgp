ByteAddressBuffer ElementCountBuffer : register(t0);

StructuredBuffer<float3> PositionBuffer : register(t1);

RWStructuredBuffer<uint3> RWBoundsMinBuffer : register (u0);
RWStructuredBuffer<uint3> RWBoundsMaxBuffer : register (u1);

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
	
	InterlockedMin(RWBoundsMinBuffer[0].x, xyz.x);
	InterlockedMin(RWBoundsMinBuffer[0].y, xyz.y);
	InterlockedMin(RWBoundsMinBuffer[0].z, xyz.z);
		
	InterlockedMax(RWBoundsMaxBuffer[0].x, xyz.x);
	InterlockedMax(RWBoundsMaxBuffer[0].y, xyz.y);
	InterlockedMax(RWBoundsMaxBuffer[0].z, xyz.z);
}

groupshared uint minBox[3];
groupshared uint maxBox[3];


/*Groupshared version, some times claimed to be faster, since out output buffer is very small
data will likely stay in the cache all the time, so it can also be slower */
[numthreads(64, 1, 1)]
void CS_ProcessGroupShared( uint3 DTid : SV_DispatchThreadID, uint3 gtid : SV_GroupThreadID )
{
	if (gtid.x == 0)
	{
		minBox[0] = minBox[1] = minBox[2] = 0xffffffff;
		maxBox[0] = maxBox[1] = maxBox[2] = 0;
	}
	GroupMemoryBarrierWithGroupSync();

	uint tid = DTid.x;
	uint elementCount = ElementCountBuffer.Load(0);
	if (tid >= elementCount)
		return;
		

	float3 p = PositionBuffer[tid];
	uint3 xyz = toSignUint(p);
	
	InterlockedMin(minBox[0], xyz.x);
	InterlockedMin(minBox[1], xyz.y);
	InterlockedMin(minBox[2], xyz.z);
	
	InterlockedMax(maxBox[0], xyz.x);
	InterlockedMax(maxBox[1], xyz.y);
	InterlockedMax(maxBox[2], xyz.z);
	
	GroupMemoryBarrierWithGroupSync();
		
	if (gtid.x == 0)
	{
		InterlockedMin(RWBoundsMinBuffer[0].x, minBox[0]);
		InterlockedMin(RWBoundsMinBuffer[0].y, minBox[1]);
		InterlockedMin(RWBoundsMinBuffer[0].z, minBox[2]);
		
		InterlockedMax(RWBoundsMaxBuffer[0].x, maxBox[0]);
		InterlockedMax(RWBoundsMaxBuffer[0].y, maxBox[1]);
		InterlockedMax(RWBoundsMaxBuffer[0].z, maxBox[2]);
	}
}