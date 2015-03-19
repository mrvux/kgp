//Shows flats normals from a model surface

cbuffer cbCamera : register(b0)
{
	float4x4 View;
	float4x4 Projection;
}

void VS(float4 p : POSITION, out float4 screenPos : SV_Position, out float3 viewPos : POSITIONVIEW)
{
	screenPos = mul(p, mul(View,Projection));
	viewPos = mul(p, View).xyz;
}

float4 PS(float4 screenPos : SV_Position, float3 viewPos : POSITIONVIEW) : SV_Target
{
	float3 dx = ddx(viewPos);
	float3 dy = ddy(viewPos);
	float3 n = normalize(cross(dx,dy));
	return float4(n,1.0f);
}

//Friendly normals view, so negative values are also shown
float4 PS_Friendly(float4 screenPos : SV_Position, float3 viewPos : POSITIONVIEW) : SV_Target
{
	float3 dx = ddx(viewPos);
	float3 dy = ddy(viewPos);
	float3 n = normalize(cross(dx,dy));
	n = n * 0.5f + 0.5f;
	return float4(n,1.0f);
}




