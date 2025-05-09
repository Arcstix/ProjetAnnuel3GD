Shader "Custom/VoronoiFallingSand"
{
    Properties
    {
        _VoronoiColor ("Ripple Color", Color) = (1, 0.8, 0.2, 1)
        _VoronoiIntensity ("Voronoi Intensity", Range(0, 1)) = 1
        _CellDensity ("Voronoi Density", Float) = 5
        _FallSpeed ("Fall Speed", Float) = 0.5
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Lighting Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _VoronoiColor;
            float _VoronoiIntensity;
            float _CellDensity;
            float _FallSpeed;

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float2 random2(float2 st)
            {
                st = float2(dot(st, float2(127.1, 311.7)),
                            dot(st, float2(269.5, 183.3)));
                return frac(sin(st) * 43758.5453123);
            }

            float voronoi(float2 st, float density)
            {
                st *= density;
                float2 i_st = floor(st);
                float2 f_st = frac(st);

                float m_dist = 1.0;

                for (int y = -1; y <= 1; y++)
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        float2 neighbor = float2(x, y);
                        float2 randOffset = random2(i_st + neighbor);
                        randOffset = 0.5 + 0.5 * sin(_Time.y + 6.2831 * randOffset);
                        float2 diff = neighbor + randOffset - f_st;
                        float dist = length(diff);
                        m_dist = min(m_dist, dist);
                    }
                }
                return m_dist;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 animatedPos = i.uv;
                animatedPos.y += _Time.y * _FallSpeed;

                // Ajout d’un léger bruit pour désaligner les points
                float2 noiseOffset = (random2(animatedPos * 10.0) - 0.5) * 0.2;
                float noise = voronoi(animatedPos + noiseOffset, _CellDensity);

                float factor = saturate(1.0 - noise) * _VoronoiIntensity;

                if (factor < 0.05)
                    discard;

                return fixed4(_VoronoiColor.rgb, factor);
            }
            ENDCG
        }
    }

    FallBack "Unlit/Transparent"
}
