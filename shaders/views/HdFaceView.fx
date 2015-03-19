StructuredBuffer<float3> FaceVertexBuffer : register(t0);

cbuffer cbCamera : register(b0)
{
	float4x4 View;
	float4x4 Projection;
}

void VS(uint iv : SV_VertexID, out float4 screenPos : SV_Position)
{
	float3 world = FaceVertexBuffer[iv];
	screenPos = mul(float4(world,1.0f), mul(View,Projection));
}

float4 PS(float4 screenPos : SV_Position) : SV_Target
{
	return 1;
}




