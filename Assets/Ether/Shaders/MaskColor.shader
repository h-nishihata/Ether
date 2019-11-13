Shader "Custom/MaskColor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Frequency ("Frequency", float) = 20
        _Fill ("Fill", Range(0, 1)) = 0.8
        _LineColor ("LineColor", Color) = (0, 0, 0, 1)
        _BGColor ("BGColor", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always
 
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
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
             
            sampler2D _MainTex;
            float _Frequency;
            float _Fill;
            fixed4 _LineColor;
            fixed4 _BGColor;
 
            float random (float2 input) { 
                return frac(sin(dot(input, float2(12.9898, 78.233))) * 43758.5453123);
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                float stripes = 1 - step(_Fill, random(floor(i.uv.y * _Frequency)));
                float r,g,b = 0;
                if(stripes < 1)
                {
                    r = _LineColor.r;
                    g = _LineColor.g;
                    b = _LineColor.b;
                }
                else if(stripes > 0)
                {
                    r = _BGColor.r;
                    g = _BGColor.g;
                    b = _BGColor.b;
                }
                return float4(r, g, b, 1);
            }
            ENDCG
        }
    }
}