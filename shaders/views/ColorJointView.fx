StructuredBuffer<float2> JointBuffer : register(t0);
StructuredBuffer<uint> JointStatusIdBuffer : register(t1);
StructuredBuffer<float4> StatusColorBuffer : register(t2);

float4 CalcPosition(float4 p, uint ii)
{
	float2 jointPosition = JointBuffer[ii];
	jointPosition /= float2(1920.0f, 1080.0f);
	jointPosition.y = 1.0f - jointPosition.y;
	jointPosition = jointPosition * 2.0f - 1.0f;
	
	//Todo: add this in cbuffer
	p.xy *= 0.1f;
	p.xy += jointPosition;
	return p;
}

void VS(float4 p : POSITION, uint ii : SV_InstanceID, out float4 screenPos : SV_Position)
{
	screenPos = CalcPosition(p,ii);
}

void VS_Color(float4 p : POSITION, uint ii : SV_InstanceID, out float4 screenPos : SV_Position, out float4 color : COLOR)
{
	screenPos = CalcPosition(p,ii);
	color = StatusColorBuffer[JointStatusIdBuffer[ii]];
}

float4 PS(float4 screenPos : SV_Position) : SV_Target
{
	return 1;
}

float4 PS_Color(float4 screenPos : SV_Position, float4 color : COLOR) : SV_Target
{
	return color;
}




