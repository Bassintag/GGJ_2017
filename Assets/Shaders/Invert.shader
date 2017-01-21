Shader "Hidden/Chroma"
{
	Properties
	{
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

			sampler2D _MainTex;

			fixed4 frag (v2f_img i) : SV_TARGET
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				return 1-col;
			}
			ENDCG
		}
	}
}
