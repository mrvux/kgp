//Constructs a filtered grid from Body index and world position

Texture2D WorldTexture : register(t0);
Texture2D BodyIndexTexture : register(t1);

#define colCount 512;
#define rowCount 424;
#define invGridSize float2(1.0f/512.0f,1.0f/424.0f);

cbuffer cbParams : register(b0)
{
	float maxEdgeLength;
	float maxDepth;
}

SamplerState linearSampler : register(s0);

struct vsOutput
{
    float3 pos : POSITION; 
    float2 uv : TEXCOORD0;
};
 
vsOutput VS(uint iv : SV_VertexID)
{
	vsOutput output;
	
	int colindex = iv % colCount;
	int rowindex = iv / rowCount;	
	float2 uv = float2(iv % colCount,iv / colCount) * invGridSize;
	uv.y = 1.0f - uv.y;
	
	float pid = BodyIndexTexture.SampleLevel(linearSampler, uv, 0).r;	
	float zadd = pid > 0.5f ? 10000.0f : 0.0f;

	output.pos =WorldTexture.SampleLevel(linearSampler, uv, 0).xyz;
	output.pos.z += zadd;
	output.uv = uv;

    return output;
} 

[maxvertexcount(3)]
void GS(triangle vsOutput input[3], inout TriangleStream<vsOutput> gsout)
{ 
	vsOutput output;
	
	float3 p1 = input[0].pos;
	float3 p2 = input[1].pos;
	float3 p3 = input[2].pos;
	
	float3 el = float3(length(p2-p1), length(p3-p1), length(p3-p2));
	float3 z = float3(p1.z,p2.z,p3.z);
	
	el = el < maxEdgeLength;
	z = z < maxDepth;
	
	if (all(el) && all(z))
	{
		[unroll]
		for (int i = 0; i < 3; i++)
		{
			gsout.Append(input[i]);
		}
		gsout.RestartStrip();
	}
}

