//Max value for valid player data in UNorm texture view
#define MAX_PLAYER 6.0f / 255.0f

Texture2D ColorTexture : register(t0);
Texture2D PlayerTexture : register(t1);
Texture2D DepthRGBMapTexture : register(t2);

SamplerState linearSampler : register(s0);

float4 ProcessPixel(float2 map, float4 rgb)
{
	//Map in depth size space, convert back into uv space
	map.x /= 512.0f;
	map.y /= 424.0f;
	float body = PlayerTexture.Sample(linearSampler,map).r;
	body = body < MAX_PLAYER;
	return rgb * body;
}

float4 PS_Sample(float4 p : SV_Position, float2 uv : TEXCOORD0): SV_Target
{
    float4 rgb = ColorTexture.Sample(linearSampler, uv);
	float2 map = DepthRGBMapTexture.Sample(linearSampler, uv);
	return ProcessPixel(map, rgb);
}

float4 PS_Load(float4 p : SV_Position, float2 uv : TEXCOORD0): SV_Target
{
    float4 rgb = ColorTexture.Load(int3(p.xy,0));
	float2 map = DepthRGBMapTexture.Load(int3(p.xy,0)).xy;
	return ProcessPixel(map, rgb);
}


