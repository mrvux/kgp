//Reconstructs Camera space position from depth and ray lookup table

Texture2D NormalizedDepthTexture : register(t0);
Texture2D<uint> RawDepthTexture: register(t0);

Texture2D<float2> RayTexture : register(t1);


float4 PS_Normalized(float4 p : SV_Position, float2 uv : TEXCOORD0) : SV_Target
{
	float d = NormalizedDepthTexture.Load(int3(p.xy,0)).r;
	d *= 65.535f; //Multiply by MAX_USHORT-1
	float2 ray = RayTexture.Load(int3(p.xy, 0)).rg;
	return float4(ray.xy*d, d,1.0f);
}

float4 PS_Raw(float4 p : SV_Position, float2 uv : TEXCOORD0) : SV_Target
{
	uint dr = RawDepthTexture.Load(int3(p.xy,0)).r;
	float d = (float)dr *0.001f;
	float2 ray = RayTexture.Load(int3(p.xy, 0)).rg;
	return float4(ray.xy*d, d,1.0f);
}









