Shader "Unlit/Zero2Shaders/GradientNoise"
{
    Properties
    {
        _ScaleAndOffset("Noise Scale and Offset", Vector) = (4, 4, 0, 0)
        _Octaves("Noise Octaves", Int) = 4
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            static const float2 s_dirs[4] = {
                float2(0.0 , 0.0),
                float2(1.0 , 0.0),
                float2(0.0 , 1.0),
                float2(1.0 , 1.0)
            };


            float random(float2 v)
            {
                return frac(sin(dot(v.xy, float2(12.9898, 78.233))) * 43758.5453123);
            }

         
            float2 randomDir(float2 v)
            {
                return float2(random(v), random(v * 2.0f)) * 2.0 - 1.0f;
            }

            float gradientNoise(float2 v)
            {
                float2 i = floor(v);
                float2 f = frac(v);
                float2 s = smoothstep(0,1,f);
                
                float2 ranDir0 = randomDir(i + s_dirs[0]);
                float2 ranDir1 = randomDir(i + s_dirs[1]);
                float2 ranDir2 = randomDir(i + s_dirs[2]);
                float2 ranDir3 = randomDir(i + s_dirs[3]);

                float cellPosition0 = f - s_dirs[0];
                float cellPosition1 = f - s_dirs[1];
                float cellPosition2 = f - s_dirs[2];
                float cellPosition3 = f - s_dirs[3];

                float p0 = dot(ranDir0, cellPosition0);
                float p1 = dot(ranDir1, cellPosition1);
                float p2 = dot(ranDir2, cellPosition2);
                float p3 = dot(ranDir3, cellPosition3);

                return lerp(lerp(p0,p1,s.x), lerp(p2,p3,s.x),s.y);
            }

            float4 _ScaleAndOffset; 
            int _Octaves;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

     
            fixed4 frag(v2f i) : SV_Target
            {
                float2 v = i.uv * _ScaleAndOffset.xy + _ScaleAndOffset.zw;
                float n = 0;
                float fq = 1.;
                float amplitude = 1.f;
                for(int i = 0; i < _Octaves; ++i)
                {
                   n+= gradientNoise(v * fq) * amplitude;
                    fq *= 2.0f;
                    amplitude *= 0.5f;
                }

                return n * 0.5f +0.5f;
            }
            ENDCG
        }
    }
}
