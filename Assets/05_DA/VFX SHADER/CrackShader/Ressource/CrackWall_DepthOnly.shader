Shader "Custom/CrackWall_DepthOnly"
{
    SubShader
    {
        Tags { "Queue"="Geometry-50" } // Très tôt, avant le sol
        Lighting Off
        ZWrite On
        ZTest LEqual
        Offset -10, -10 // Push dans le DepthBuffer pour ne pas flicker
        Cull Off

        Pass
        {
            ColorMask 0 // Ne rend rien à l’écran

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
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return 0;
            }
            ENDCG
        }
    }

    FallBack Off
}
