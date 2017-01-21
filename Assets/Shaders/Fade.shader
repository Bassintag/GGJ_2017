Shader "Custom/Fade"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Color) = (0,0,0,1)
		_Fade ("Fade", Range(0.0,1.0)) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : POSITION;
			};

			sampler2D _MainTex;
			fixed4 _Color;
			float _Fade;
			
			v2f vert (float2 uv : TEXCOORD0, float4 vertex : POSITION)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(vertex);
				o.uv = uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				return lerp(col,_Color,_Fade);
			}
			ENDCG
		}
	}
}
