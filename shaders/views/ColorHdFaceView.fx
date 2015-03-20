StructuredBuffer<float3> FaceVertexBuffer : register(t0);
StructuredBuffer<float2> FaceColorSpaceBuffer : register(t1);

Texture2D RGBTexture : register(t0);

SamplerState linearSampler : register(s0);

cbuffer cbCamera : register(b0)
{
	float4x4 View;
	float4x4 Projection;
}

void VS(uint iv : SV_VertexID, out float4 screenPos : SV_Position, out float2 uv : TEXCOORD0)
{
	float3 world = FaceVertexBuffer[iv];
	screenPos = mul(float4(world,1.0f), mul(View,Projection));
	uv = FaceColorSpaceBuffer[iv];
	uv.x /= 1920.0f;
	uv.y /= 1080.0f;
}

float4 PS(float4 screenPos : SV_Position, float2 uv : TEXCOORD0) : SV_Target
{
	return RGBTexture.Sample(linearSampler, uv);
}




