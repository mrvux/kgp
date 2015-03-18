StructuredBuffer<float2> JointBuffer : register(t0);

void VS(float4 p : POSITION, uint ii : SV_InstanceID, out float4 screenPos : SV_Position)
{
	float2 jointPosition = JointBuffer[ii];
	jointPosition /= float2(1920.0f, 1080.0f);
	jointPosition.y = 1.0f - jointPosition.y;
	jointPosition = jointPosition * 2.0f - 1.0f;
	
	//Todo: add this in cbuffer
	p.xy *= 0.1f;
	
	screenPos = p;
	screenPos.xy += jointPosition;
}

float4 PS(float4 screenPos : SV_Position) : SV_Target
{
	return 1;
}




