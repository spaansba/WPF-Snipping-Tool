sampler2D inputSampler : register(s0);

float Random(float2 uv)
{
    return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453) / 43758.5453;
}

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float3 noise = tex2D(inputSampler, uv).rgb;
    float rand = Random(uv);
    float2 jitter = rand * 0.01;
    
    float3 color = noise * 0.8 + rand * 0.8;
    
    return float4(color, 1);
}

technique
{
    pass
    {
        PixelShader = compile ps_2_0 main();
    }
}