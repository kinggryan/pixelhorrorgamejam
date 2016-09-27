Shader "Other/CustomDitherShader" {
	Properties{
		_MainTex("Render Input", 2D) = "white" {}
		_ColorCount("Mixed Color Count", float) = 4
		_PaletteHeight("Palette Height", float) = 128
		_PaletteTex("Palette", 2D) = "black" {}
		_DitherSize("Dither Size (Width/Height)", float) = 8
		_DitherTex("Dither", 2D) = "black" {}
	}

	SubShader{
		Pass{
			ZTest Always Cull Off ZWrite Off

			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "CGIncludes/Dithering.cginc"

			sampler2D _MainTex;
			sampler2D _PaletteTex;
			sampler2D _DitherTex;
			float _ColorCount;
			float _PaletteHeight;
			float _DitherSize;

			fixed4 frag(v2f_img i) : SV_Target
			{
				fixed4 original = tex2D(_MainTex, i.uv);
				fixed4 output = fixed4(1, 1, 1, 1);
				float4 ditherPos = GetDitherPosFromScreenPos(float4(i.uv.x,i.uv.y,0,1), _DitherSize);
				output.rgb = GetDitherColor(original.rgb, _DitherTex, _PaletteTex, _PaletteHeight, ditherPos, _ColorCount);
				return output;
			}
			ENDCG
		}
	}

	Fallback off
}
