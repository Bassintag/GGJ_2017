Shader "Custom/ParticleFade"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Fade ("Fade", Range(0.0,1.0)) = 0.0
	}
	SubShader
	{
		Tags {
			"RenderType"="Transparent"
		}

		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 pos : POSITION;
			};

			float _Fade;
			sampler2D _MainTex;
			
			v2f vert (float2 uv : TEXCOORD0, float4 vertex : POSITION)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(vertex);
				o.uv = uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				col.a *= 1 - _Fade;
				return col;
			}
			ENDCG
		}
	}
}
