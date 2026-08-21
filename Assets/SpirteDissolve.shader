Shader "Custom/SpriteDissolve"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _DissolveAmount ("Dissolve Amount", Range(0, 1)) = 1
        _EdgeWidth ("Edge Width", Range(0, 0.2)) = 0.05
        _EdgeColor ("Edge Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags
        { 
            "Queue"="Transparent" 
            "RenderType"="Transparent" 
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off

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
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _DissolveAmount;
            float _EdgeWidth;
            float4 _EdgeColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color * _Color;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * i.color;
                
                float3 objPos = mul(unity_ObjectToWorld, float4(0,0,0,1)).xyz;
                float height = i.worldPos.y - objPos.y;
                
                float dissolve = step(height, _DissolveAmount - 0.5);
                float edge = step(height, _DissolveAmount + _EdgeWidth - 0.5) - dissolve;
                
                col.rgb = lerp(col.rgb, _EdgeColor.rgb, edge * _EdgeColor.a);
                col.a *= dissolve;
                col.rgb *= col.a;
                
                return col;
            }
            ENDCG
        }
    }
}