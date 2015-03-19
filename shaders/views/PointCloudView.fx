Texture2D worldTexture : register(t0);

cbuffer cbCamera : register(b0)
{
	float4x4 View;
	float4x4 Projection;
}

//Use iv/ii combination to process a fake grid render
void VS(uint iv : SV_VertexID, uint ii: SV_InstanceID, out float4 screenPos : SV_Position)
{
	float3 world = worldTexture.Load(int3(iv, ii,0)).xyz;	
	screenPos = mul(float4(world,1.0f), mul(View,Projection));
}

float4 PS(float4 screenPos : SV_Position) : SV_Target
{
	return 1;
}




