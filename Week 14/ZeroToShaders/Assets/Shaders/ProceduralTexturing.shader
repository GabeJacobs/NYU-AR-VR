// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/ProceduralTexturing"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _ScaleAndOffset("Noise Scale and Offset", Vector) = (4, 4, 0, 0)
        _Octaves("Noise Octaves", Int) = 5
        _Color1("Color 1", Color) = (1,0,0,1)
        _Color2("Color 1", Color) = (0,0,1,1)

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.5

        struct Input
        {
            float2 uv_MainTex;
        };

        float4 _ScaleAndOffset;
        int _Octaves;
        float4 _Color1;
        float4 _Color2;

        sampler2D _MainTex;

        float remap(float v, float a1, float a2, float b1, float b2)
        {
            return b1 + (v - a1) * (b2 - b1) / (a2 - a1);
        }

        void OctaveGradientNioise_float(){}

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
           float n = octaveNoise(IN.uv_MainTex);
           fixed4 col = lerp(_Color1, _Color2, n);
           o.Albedo = col.rgb;
           o.Alpha = 1.0f;
           o.Metallic  = smoothstep(0.3f, 0.0f, n);
           o.Smoothness = remap(n,0.0,1.0,0.23,1.0);
           o.Emission = smoothstep(0.5,0.0,n) * _Color1;
            
        }
        ENDCG
    }
    FallBack "Diffuse"
}
