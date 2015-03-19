//Texture version
Texture2D worldTexture : register(t0);

Texture2D ColorTexture : register(t1);
Texture2D RGBDepthMapTexture: register(t2);

SamplerState linearSampler : register(s0);

//Indirect version
StructuredBuffer<float3> FilteredPointCloudBuffer : register(t0);
StructuredBuffer<float4> ColorBuffer : register(t1);

cbuffer cbCamera : register(b0)
{
	float4x4 View;
	float4x4 Projection;
};

float4 ProcessColor(float2 map)
{
	map.x /= 1920.0f;
	map.y /= 1080.0f;
	float4 rgb = ColorTexture.SampleLevel(linearSampler,map, 0);
	return rgb;
}

//Use iv/ii combination to process a fake grid render
void VS(uint iv : SV_VertexID, uint ii: SV_InstanceID, out float4 screenPos : SV_Position, out float4 color : COLOR)
{
	float3 world = worldTexture.Load(int3(iv, ii,0)).xyz;	
	screenPos = mul(float4(world,1.0f), mul(View,Projection));
	
	float2 map = RGBDepthMapTexture.Load(int3(iv, ii,0)).xy;
	color = ProcessColor(map);
	
}

//Indirect version, we already picked elements we wanted in a buffer
void VS_Indirect(uint iv : SV_VertexID,out float4 screenPos : SV_Position, out float4 color : COLOR)
{
	float3 world = FilteredPointCloudBuffer[iv];	
	screenPos = mul(float4(world,1.0f), mul(View,Projection));
	color = ColorBuffer[iv];
}

float4 PS(float4 screenPos : SV_Position, float4 color : COLOR) : SV_Target
{
	return color;
}




