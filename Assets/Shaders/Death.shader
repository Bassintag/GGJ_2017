Shader "Hidden/Death"
{
	Properties
	{
		_Aberration ("Aberration", Range(0.0,1.0)) = 0
		_Fade ("Fade", Range(0.0,1.0)) = 0
		_MainTex ("Tex", 2D) = "white" {}
		_FadeColor ("Fade Color", Color) = (0,0,0,1)
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
			float _Fade;
			sampler2D _MainTex;
			fixed4 _FadeColor;

			fixed4 frag (v2f_img i) : SV_TARGET
			{
				fixed4 c;
				c.r = tex2D(_MainTex,i.uv + _Aberration / 2).r;
				c.g = tex2D(_MainTex,i.uv).g;
				c.b = tex2D(_MainTex,i.uv - _Aberration / 2).b;
				c.a = tex2D(_MainTex, i.uv).a;
				return lerp(c, _FadeColor, _Fade);
			}
			ENDCG
		}
	}
}
