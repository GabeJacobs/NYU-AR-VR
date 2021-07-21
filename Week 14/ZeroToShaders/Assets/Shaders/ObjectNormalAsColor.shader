Shader "Unlit/Zero2Shaders/ObjectNormalAsColor"
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
                float3 objectNormal : TEXCOORD0;
                //use interpolator TEXCOORD0 to interpolate the normal to the fragment
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.objectNormal = v.normal;
                //pass the normal along

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // transform the normal to a visible range 
                float3 norm = i.objectNormal;
                norm *= 0.5f;
                norm += 0.5f;

                return float4(norm, 1);
                //use normal as a color

            }
            ENDCG
        }
    }
}
