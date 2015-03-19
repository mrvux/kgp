Texture2DArray WorldTexture : register(t0);
Texture2DArray<uint> BodyIndexTexture : register(t1);
StructuredBuffer<float4x4> MatrixBuffer : register(t2);

//Append buffer version
AppendStructuredBuffer<float3> AppendPositionBuffer : register(u0);

//Counter buffer version
RWStructuredBuffer<float3> RWPositionBuffer : register(u0);
RWStructuredBuffer<float3> RWBodyIndexBuffer : register(u1);

bool CheckPlayer(uint pid)
{
	return pid < 8;
}

void PS_Position(float4 p : SV_Position, uint sliceIndex : SV_RenderTargetArrayIndex)
{
	uint pid = BodyIndexTexture.Load(int4(p.xy,0,sliceIndex));
	float3 w = WorldTexture.Load(int4(p.xy,0,sliceIndex)).xyz;
	
	if (CheckPlayer(pid) && length(w) < 500)
	{
		w = mul(float4(w,1.0f),MatrixBuffer[sliceIndex]).xyz;
		RWAppendEmitBuffer.Append(w);
	}
}

void PS_PositionIndex(float4 p : SV_Position, uint sliceIndex : SV_RenderTargetArrayIndex)
{
	uint pid = BodyIndexTexture.Load(int4(p.xy,0,sliceIndex));
	float3 w = WorldTexture.Load(int4(p.xy,0,sliceIndex)).xyz;
	
	if (CheckPlayer(pid) && length(w) < 500)
	{
		w = mul(float4(w,1.0f),MatrixBuffer[sliceIndex]).xyz;
		uint idx = RWPositionBuffer.IncrementCounter();
		RWPositionBuffer[idx] = w;
		RWBodyIndexBuffer[idx] = pid;
	}
}


