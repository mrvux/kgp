#define K2_HdFace_VertexCount 1347

StructuredBuffer<float2> FaceVertexBuffer : register(t0);
StructuredBuffer<uint> FaceLookupBuffer : register(t1);

Texture2D RGBTexture : register(t0);
SamplerState linearSampler : register(s0);

void VS_Simple(uint iv : SV_VertexID,uint ii : SV_InstanceID, out float4 screenPos : SV_Position, out float2 uv : TEXCOORD0)
{
	float2 facePos = FaceVertexBuffer[ii * K2_HdFace_VertexCount + iv];
	facePos.x /= 1920.0f;
	facePos.y /= 1080.0f;
	//Set uv to face position
	uv = facePos;

	facePos.y = 1.0f -facePos.y;
	facePos = facePos * 2.0f - 1.0f;
	screenPos = float4(facePos, 0.0f, 1.0f);
}

void VS_Indexed(uint iv : SV_VertexID,uint ii : SV_InstanceID, out float4 screenPos : SV_Position, out float2 uv : TEXCOORD0)
{
	float2 facePos = FaceVertexBuffer[ii * K2_HdFace_VertexCount + iv];
	facePos.x /= 1920.0f;
	facePos.y /= 1080.0f;
	facePos.y = 1.0f -facePos.y;
	facePos = facePos * 2.0f - 1.0f;
	screenPos = float4(facePos, 0.0f, 1.0f);

	float2 faceUv = FaceVertexBuffer[FaceLookupBuffer[ii] * K2_HdFace_VertexCount + iv];
	faceUv.x /= 1920.0f;
	faceUv.y /= 1080.0f;
	uv = faceUv;
}

float4 PS(float4 screenPos : SV_Position, float2 uv : TEXCOORD0) : SV_Target
{
	return RGBTexture.Sample(linearSampler, uv);
}