cbuffer cbColor : register(b0)
{
	float4 color;
}

float4 PS(float4 screenPos : SV_Position) : SV_Target
{
	return color;
}