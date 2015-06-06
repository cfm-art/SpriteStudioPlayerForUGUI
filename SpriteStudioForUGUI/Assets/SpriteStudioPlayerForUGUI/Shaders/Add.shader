Shader "aSpriteStudio/Add"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255

		_ColorMask ("Color Mask", Float) = 15
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
		
		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp] 
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Fog { Mode Off }
		Blend SrcColor One, SrcAlpha One
		ColorMask [_ColorMask]

		Pass
		{
		CGPROGRAM
			#pragma vertex vert_add
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "Common.cginc"

            v2f vert_add(appdata_t IN)
            {
                // XXX: ‚È‚º‚©’¸“_‚Ìƒ¿‚ª0.2ˆÊŒ¸‚Á‚Ä‚éŠ´‚¶‚ª‚·‚é
	            v2f OUT = vert( IN );
                OUT.color.rgb = OUT.color.rgb * (IN.color.a + 0.2);
                return OUT;
            }
		ENDCG
		}
	}
	
	FallBack "UI/Default"
}
