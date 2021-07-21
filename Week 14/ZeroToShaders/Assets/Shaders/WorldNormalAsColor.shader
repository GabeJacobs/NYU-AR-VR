Shader "Unlit/Zero2Shaders/WorldNormalAsColor"
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
                float3 normal : NORMAL;
                //use the NORMAL semantic to get the normal attribute 
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
                //use TEXCOORD0 interpolator to interpolate normal to fragment shader
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                
                //https://docs.unity3d.com/Manual/SL-BuiltinFunctions.html

                //transform object normal to world

                //pass normal along
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {

                float3 worldNorm = i.worldNormal * .5f + .5f;

                return float4(worldNorm, 1.0f);
                //transform world normal to visible range

                //use world normal as a color
            }
            ENDCG
        }
    }
}
