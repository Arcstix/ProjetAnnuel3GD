Shader "Custom/DepthMaskOnly"
{
    SubShader
    {
        Tags { "Queue"="Geometry-50" } // Très tôt dans la pile de rendu
        Lighting Off
        ZWrite On
        ZTest LEqual
        Offset -10, -10   // On force la priorité dans le DepthBuffer
        Cull Off

        Pass
        {
            ColorMask 0   // Ne rend pas à l’écran, juste dans le ZBuffer

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