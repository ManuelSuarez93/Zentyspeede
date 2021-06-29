Shader "VertexDisplacement" 
{
	Properties
	{
		_Color("Tint", Color) = (0, 0, 0, 1)
		_MainTex("Texture", 2D) = "white" {}
		_Smoothness("Smoothness", Range(0, 1)) = 0
		_Metallic("Metalness", Range(0, 1)) = 0
		[HDR] _Emission("Emission", color) = (0,0,0)
		_Amplitude("Wave Size", Range(0,1)) = 0.4
		_Frequency("Wave Frequency", Range(1, 10)) = 2
		_AnimationSpeed("Animation Speed", Range (0,5)) = 1

	}
		SubShader
		{
			Tags{ "RenderType" = "Opaque" "Queue" = "Geometry"}

			CGPROGRAM

			#pragma surface surf Standard fullforwardshadows vertex:vert addshadow
			#pragma target 3.0

			sampler2D _MainTex;
			fixed4 _Color;
			float _AnimationSpeed;

			half _Smoothness;
			half _Metallic;
			half3 _Emission;

			float _Amplitude;
			float _Frequency;

			struct Input 
			{
				float2 uv_MainTex;
			};

			void vert(inout appdata_full data)
			{
				float4 modifiedPos = data.vertex;
				modifiedPos.y += sin(data.vertex.x * _Frequency + _Time.y * _AnimationSpeed) * _Amplitude; 
				modifiedPos.x += sin(data.vertex.y * _Frequency + _Time.y * _AnimationSpeed) * _Amplitude;
				modifiedPos.z += sin(data.vertex.z * _Frequency + _Time.y * _AnimationSpeed) * _Amplitude;

				float3 posPlusTangent = data.vertex + data.tangent * 0.01;
				posPlusTangent += sin(posPlusTangent.x * _Frequency + _Time.y * _AnimationSpeed) * _Amplitude;

				float3 bitangent = cross(data.normal, data.tangent);
				float3 posPlusBitangent = data.vertex + bitangent * 1;
				posPlusBitangent.y += sin(posPlusBitangent.x * _Frequency + _Time.y * _AnimationSpeed) * _Amplitude;

				float3 modifiedTangent = posPlusTangent - modifiedPos;
				float3 modifiedBitangent = posPlusBitangent - modifiedPos;

				float3 modifiedNormal = cross(modifiedTangent, modifiedBitangent);

				data.normal = normalize(modifiedNormal);

				data.vertex = modifiedPos;
			}

			void surf(Input i, inout SurfaceOutputStandard o) 
			{
				fixed4 col = tex2D(_MainTex, i.uv_MainTex);
				col *= _Color;
				o.Albedo = col.rgb;
				o.Metallic = _Metallic;
				o.Smoothness = _Smoothness;
				o.Emission = _Emission;
			}
			ENDCG
		}
			FallBack "Standard"
}