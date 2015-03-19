struct gsInput
{
	float4 p : POSITION;
	uint sliceIndex : SLICEINDEX;
};

struct gsOutput
{
	float4 p : SV_Position;
	uint index : SV_RenderTargetArrayIndex;
};

//Triangle version
gsInput VS_Tri( uint vertexID : SV_VertexID,uint ii : SV_InstanceID)
{
    float2 uv = float2((vertexID << 1) & 2, vertexID & 2);
    output.position = float4(uv * float2(2.0f, -2.0f) + float2(-1.0f, 1.0f), 0.0f, 1.0f);
	output.sliceIndex = ii;
	return output;
}

//Quad version
gsInput VS_Quad(float4 p : POSITION, uint ii : SV_InstanceID)
{
	gsInput output;
	output.p = p;
	output.sliceIndex = ii;
	return output;
}

[maxvertexcount(3)]
void GS(triangle gsInput input[3], inout PointStream<gsOutput> gsout)
{ 
	gsOutput output;
	output.index = input[0].sliceIndex;
	
	[unroll]
	for (uint i = 0; i < 3; i++)
	{
		output.p = input[0].p;
		gsout.Append(output);
	}
	gsout.RestartStrip();
}