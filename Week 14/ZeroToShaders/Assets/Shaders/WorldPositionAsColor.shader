﻿Shader "Unlit/Zero2Shaders/WorldPositionAsColor"
{
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
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldPosition : TEXCOORD0;
                //Use TEXCOORD0 interpolator to pass object space position to fragment shader
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                
                //https://docs.unity3d.com/Manual/SL-UnityShaderVariables.html
                //transform object space to world space
                o.worldPosition = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return float4(i.worldPosition, 1.0);
            }
            ENDCG
        }
    }
}
