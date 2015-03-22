StructuredBuffer<float2> FaceVertexBuffer : register(t0);

void VS(uint iv : SV_VertexID, out float4 screenPos : SV_Position)
{
	float2 facePos = FaceVertexBuffer[iv];
	facePos.x /= 1920.0f;
	facePos.y /= 1080.0f;
	facePos.y = 1.0f -facePos.y;
	facePos = facePos * 2.0f - 1.0f;
	screenPos = float4(facePos, 0.0f, 1.0f);
}

float4 PS(float4 screenPos : SV_Position) : SV_Target
{
	return float4(1,1,1,0.2f);
}




