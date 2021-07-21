Shader "Unlit/Zero2Shaders/Pow"
{
    Properties
    {
        _Power("Power", Range(1,10)) = 5
        _Position1("Position 1", Range(0,1)) = .5
        _Position2("Position 2", Range(0,1)) = .5
        _Position3("Position 3", Range(0,1)) = .5
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

            float _Power;
            float _Position1;
            float _Position2;
            float _Position3;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return float4 (pow(1 - length(float2(_Position1, _Position1) - i.uv), _Power),pow(1 - length(float2(_Position2, _Position2) - i.uv), _Power),pow(1 - length(float2(_Position3, _Position3) - i.uv), _Power),1);
            }
            ENDCG
        }
    }
}
