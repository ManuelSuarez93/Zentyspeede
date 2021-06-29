Shader "Tutorial/001-008BlendShader2"
{
    //show values to edit in inspector
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _SecondaryTex("Secondary Texture", 2D) = "black" {}
        _BlendTex ("BlendTexture", 2D) = "grey"{}
    }

        SubShader
    {
        //the material is completely non-transparent and is rendered at the same time as the other opaque geometry
        Tags{ "RenderType" = "Opaque" "Queue" = "Geometry" }

        Pass
        {
            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vert;
            #pragma fragment frag;
            //texture and transforms of the texture
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _SecondaryTex;
            float4 _SecondaryTex_ST;
            sampler2D _BlendTex;
            float4 _BlendTex_ST;

            float _BlendTexture;
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            //the data thats passed from the vertex to the fragment shader and interpolated by the rasterizer
            struct v2f
            {
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_TARGET
            {
                float2 main_uv = TRANSFORM_TEX(i.uv, _MainTex);
                float2 secondary_uv = TRANSFORM_TEX(i.uv, _SecondaryTex);
                float2 blend_uv = TRANSFORM_TEX(i.uv, _BlendTex);

                fixed4 main_color = tex2D(_MainTex, main_uv);
                fixed4 secondary_color = tex2D(_SecondaryTex, secondary_uv);
                fixed4 blend_color = tex2D(_BlendTex, blend_uv);

                fixed blend_value = blend_color.r;
                fixed4 lerptexture = lerp(main_color, secondary_color, blend_value);
                return lerptexture;
            }
            ENDCG
        }
    }
}

