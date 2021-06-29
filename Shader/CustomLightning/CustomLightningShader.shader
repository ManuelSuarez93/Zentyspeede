Shader "Custom/NewSurfaceShader"
{
    Properties
    {
        _Color("Tint", Color) = (0,0,0,1)
        _MainTex("Texture", 2D) = "white" {}
        [HDR]_Emission("Emission", Color) = (0,0,0)
        _Ramp("Toon Ramp", 2D) = "white"{}
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" "Queue" = "Geometry" }
            LOD 100

            CGPROGRAM

            #pragma surface surf Custom fullforwardshadows
            #pragma target 3.0

            sampler2D _Ramp;
            sampler2D _MainTex; //Texture
            float4 _Color; //Color of the texture
            half _Smoothness;
            half _Emission;

            struct Input
            {
                float2 uv_MainTex;
            };

            float4 LightingCustom(SurfaceOutput s, float3 lightDir, float atten)
            {
                float towardsLight = dot(s.Normal, lightDir);
                towardsLight = towardsLight * 0.5 + 0.5;

                float3 lightIntenisty = tex2D(_Ramp, towardsLight).rgb;

                float4 col;
                col.rgb = lightIntenisty * s.Albedo * atten * _LightColor0.rgb;
                col.a = s.Alpha;

                return col;
            }

            void surf(Input i, inout SurfaceOutput o)
            {
                fixed4 col = tex2D(_MainTex, i.uv_MainTex);
                col *= _Color;
                o.Albedo = col.rgb;
                o.Emission = _Emission;
            }

            ENDCG
        }
            FallBack "Standard"
}
