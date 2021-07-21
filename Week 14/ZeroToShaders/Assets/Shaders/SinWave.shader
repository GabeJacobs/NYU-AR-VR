﻿Shader "Unlit/Zero2Shaders/SinWave"
{
    Properties
    {
        _Frequency("frequency", Range(1, 1000)) = 1
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            static const float PI = 3.14159265f;
            static const float PI_2 = PI*2;

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

            float _Frequency;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return sin(i.uv.x * PI_2 * _Frequency) * .5 + .5;
                //use sin function for color
            }
            ENDCG
        }
    }
}
