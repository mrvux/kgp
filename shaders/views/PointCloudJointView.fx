#define K2_MaxJoints 25

Texture2D worldTexture : register(t0);
/*Colorizes a filtered point cloud using closest joint and color table
NOTE: This supports a single body for now */

StructuredBuffer<float3> FilteredPointCloudBuffer : register(t0);
StructuredBuffer<float3> JointPositionBuffer : register(t1);
StructuredBuffer<float4> JointColorTableBuffer : register(t2);

cbuffer cbCamera : register(b0)
{
	float4x4 View;
	float4x4 Projection;
}

void VS(uint iv : SV_VertexID,out float4 screenPos : SV_Position, out float4 color : COLOR)
{
	float3 world = FilteredPointCloudBuffer[iv];
	
	//Set closest to 0
	uint closest = 0;
	float3 diff = JointPositionBuffer[0] - world;
	float minDist = dot(diff,diff);
	
	for (uint i = 1; i < K2_MaxJoints; i++)
	{
		diff = JointPositionBuffer[i] - world;
		float d = dot(diff,diff);
		if (d < minDist)
		{
			minDist = d;
			closest = i;
		}
	}
	color = JointColorTableBuffer[closest];
	screenPos = mul(float4(world,1.0f), mul(View,Projection));
}

float4 PS(float4 screenPos : SV_Position, float4 color : COLOR) : SV_Target
{
	return color;
}




