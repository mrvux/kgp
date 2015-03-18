Texture2D BodyIndexNormalizedTexture : register(t0);

Texture2D<uint> BodyIndexRawTexture : register(t0);

SamplerState linearSampler : register(s0);

float4 PS_NormalizedView(float4 p : SV_Position, float2 uv : TEXCOORD0): SV_Target
{
	float body = BodyIndexNormalizedTexture.Sample(linearSampler, uv).r;
	return body < 0.5f;
}

float4 PS_RawView(float4 p : SV_Position, float2 uv : TEXCOORD0): SV_Target
{
	uint body = BodyIndexRawTexture.Load(int3(p.xy,0));
	return body < 7;
}




