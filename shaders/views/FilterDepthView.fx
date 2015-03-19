//Max value for valid player data in UNorm texture view
#define MAX_PLAYER 6.0f / 255.0f

Texture2D ColorTexture : register(t0);
Texture2D PlayerTexture : register(t1);
Texture2D RGBDepthMapTexture: register(t2);

SamplerState linearSampler : register(s0);

float4 ProcessPixel(float2 map, float body)
{
	body = body < MAX_PLAYER;
	//Map in depth size space, convert back into uv space
	map.x /= 1920.0f;
	map.y /= 1080.0f;
	float4 rgb = ColorTexture.Sample(linearSampler,map);
	return rgb * body;
}

float4 PS_Sample(float4 p : SV_Position, float2 uv : TEXCOORD0): SV_Target
{
	float body = PlayerTexture.Sample(linearSampler, uv);
	float2 map = RGBDepthMapTexture.Sample(linearSampler, uv);
	return ProcessPixel(map,body);
}

float4 PS_Load(float4 p : SV_Position, float2 uv : TEXCOORD0): SV_Target
{
	float body = PlayerTexture.Load(int3(p.xy,0)).r;
	float2 map = RGBDepthMapTexture.Load(int3(p.xy,0)).xy;
	return ProcessPixel(map,body);
}






