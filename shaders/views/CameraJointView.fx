StructuredBuffer<float3> JointBuffer : register(t0);
StructuredBuffer<uint> JointStatusIdBuffer : register(t1);
StructuredBuffer<float4> StatusColorBuffer : register(t2);

cbuffer cbCamera : register(b0)
{
	float4x4 View;
	float4x4 Projection;
}

float4 CalcPosition(float4 p, uint ii)
{
	float3 jointPosition = JointBuffer[ii];
	p.xyz += jointPosition;
	return mul(p, mul(View,Projection));
}

void VS_Color(float4 p : POSITION, uint ii : SV_InstanceID, out float4 screenPos : SV_Position, out float4 color : COLOR)
{
	screenPos = CalcPosition(p,ii);
	color = StatusColorBuffer[JointStatusIdBuffer[ii]];
}

float4 PS_Color(float4 screenPos : SV_Position, float4 color : COLOR) : SV_Target
{
	return color;
}




