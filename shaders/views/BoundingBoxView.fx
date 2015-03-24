struct BoundingBox
{
	float3 boundsMin;
	float3 boundsMax;
};

StructuredBuffer<BoundingBox> BoundsBuffer : register(t0);

cbuffer cbCamera : register(b0)
{
	float4x4 View;
	float4x4 Projection;
}

//Assumes that we have a -1 to 1 box
void VS(float4 p : POSITION, uint ii: SV_InstanceID, out float4 screenPos : SV_Position)
{
	BoundingBox box = BoundsBuffer[ii];
	float3 scale = box.boundsMax - box.boundsMin;
	float3 center = scale * 0.5f;
	p.xyz -= center;
	p.xyz *= scale;
	screenPos = p;
}

float4 PS(float4 screenPos : SV_Position) : SV_Target
{
	return 1;
}




