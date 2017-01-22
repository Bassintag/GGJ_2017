Shader "Custom/Wave"
{
	Properties
	{
		_Range("Range", Range(0.0, 1.0)) = 0.5
		_Width("Width", Range(0.0, 1.0)) = 0.1
		_BWidth("Border Width", Range(0.0, 1.0)) = 0.5
		_Fade("Fade", Range(0.0, 1.0)) = 0.5
		_Size("Size", Float) = 1.
		_Color("Color", Color) = (1,1,1,1)
		_BColor("Border Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags
		{
			"Queue"="Transparent"
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
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};
			
			float _Range;
			float _Width;
			float _Fade;
			float _BWidth;
			float _Size;
			fixed4 _Color;
			fixed4 _BColor;

			v2f vert (float2 uv : TEXCOORD0, float4 vertex : POSITION)
			{
				v2f o;
				o.uv = uv;
				o.pos = UnityObjectToClipPos(vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_TARGET
			{
				float dist = sqrt((pow(i.uv.x - 0.5, 2) + pow(i.uv.y - 0.5, 2))) / _Size;
				if (dist < _Range - _Width || dist > _Range)
				{
					discard;
				}
				fixed4 o = lerp(_Color, _BColor, 1-(_Range-dist)/_BWidth);
				fixed4 fade = float4(1,1,1,1-_Range*_Fade);
				o.a *= 1 - (_Range-dist) / _Width;
				return o*fade;
			}
			ENDCG
		}
	}
}
