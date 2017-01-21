Shader "Hidden/Chroma"
{
	Properties
	{
		_Aberration ("Aberration", Range(0.0,1.0)) = 0
		_MainTex ("Tex", 2D) = "white" {}
	}
	SubShader
	{
		Tags
		{
			"Queue"="Transparent"
		}
		Pass
		{
			CGPROGRAM
			#pragma fragment frag
			#pragma vertex vert_img

			#include "UnityCG.cginc"

			float _Aberration;
			sampler2D _MainTex;

			fixed4 frag (v2f_img i) : SV_TARGET
			{
				fixed4 o;
				o.r = tex2D(_MainTex,i.uv + _Aberration / 2).r;
				o.g = tex2D(_MainTex,i.uv).g;
				o.b = tex2D(_MainTex,i.uv - _Aberration / 2).b;
				o.a = tex2D(_MainTex, i.uv).a;
				return o;
			}
			ENDCG
		}
	}
}
