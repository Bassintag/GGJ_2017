Shader "Custom/Wave"
{
	Properties
	{
		_Range("Range", Range(0.0, 1.0)) = 0.5
		_Width("Width", Range(0.0, 1.0)) = 0.1
		_Intensity("Intensity", Range(0.0, 1.0)) = 0.5
		_Size("Size", Float) = 1.
		_Color("Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" }
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
				float4 color : COLOR;
				float2 uv : TEXCOORD0;
			};
			
			float _Range;
			float _Width;
			float _Intensity;
			float _Size;
			fixed4 _Color;

			v2f vert (float2 uv : TEXCOORD0, float4 vertex : POSITION)
			{
				v2f o;
				o.uv = uv;
				o.pos = UnityObjectToClipPos(vertex);
				o.color = _Color;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_TARGET
			{
				fixed4 o = i.color;
				float dist = sqrt((pow(i.uv.x - 0.5, 2) + pow(i.uv.y - 0.5, 2))) / _Size;
				if (dist < _Range - _Width / 2 || dist > _Range + _Width / 2)
				{
					discard;
				}
				float c = (dist-_Range)/(_Width/2);
				float4 fade = float4(1,1,1,c);
				c *= _Intensity;
				return float4(c,c,c,1) + o * fade;
			}
			ENDCG
		}
	}
}
