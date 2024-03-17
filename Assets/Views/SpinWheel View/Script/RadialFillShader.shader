Shader "Custom/RadialFillShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FillAmount ("Fill Amount", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _FillAmount;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Center of the image
                float2 center = float2(0.5, 0.5);

                // Calculate the distance from the center to the current UV coordinate
                float dist = length(center - i.uv);

                // Calculate the fill value based on the distance
                float fillValue = saturate(dist * 2.0f);

                // Apply the fill amount
                fillValue = 1.0 - _FillAmount + _FillAmount * fillValue;

                return tex2D(_MainTex, i.uv) * fillValue;
            }
            ENDCG
        }
    }
}
