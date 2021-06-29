Shader "Tutorial/001-008BlendColorTExture"
{
    //show values to edit in inspector
    Properties
    {
        _Color("Color", Color) = (0, 0, 0, 1)
        _SecondaryColor("Second Color", Color) = (0,0,0,1)
        _BlendColor("Blend Color value", Range(0,1)) = 0
        _BlendTexture("Blend Texture value", Range(0,1)) = 0
        _MainTex("Texture", 2D) = "white" {}
        _SecondaryTex("Secondary Texture", 2D) = "black" {}
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

          
            float _BlendColor;
            float _BlendTexture;
            //tint of the texture
            fixed4 _Color;
            fixed4 _SecondaryColor;
            //the mesh data thats read by the vertex shader
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

                fixed4 main_color = tex2D(_MainTex, main_uv);
                fixed4 secondary_color = tex2D(_SecondaryTex, secondary_uv);

                fixed4 lerpcol = lerp(_Color,_SecondaryColor, _BlendColor);
                fixed4 lerptexture = lerp(main_color, secondary_color, _BlendTexture);
                fixed4 col = lerpcol + lerptexture;
                return col;
            }
            ENDCG
        }
    }
}
  
