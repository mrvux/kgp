Texture2D WorldTexture : register(t0);
Texture2D<uint> BodyIndexTexture : register(t1);

AppendStructuredBuffer<float3> AppendPointCloudBuffer : register(u0);

[numthreads(8, 8, 1)]
void CS_Filter(uint3 i : SV_DispatchThreadID)
{
	if (i.x >= 512 || i.y >= 424) { return; }

	uint pid = BodyIndexTexture[i.xy];
	float3 w = WorldTexture[i.xy].xyz;
	if (pid < 8 && length(w) < 500)
	{
		AppendPointCloudBuffer.Append(w);
	}
}