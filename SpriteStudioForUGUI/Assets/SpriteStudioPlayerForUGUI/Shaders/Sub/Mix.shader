// テクセルを減算・頂点カラーをαブレンド
Shader "aSpriteStudio/Sub/Mix"
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
		//Blend OneMinusDstColor Zero, SrcAlpha One
		Blend SrcColor DstColor, SrcAlpha OneMinusSrcAlpha
        BlendOp RevSub, Add

		ColorMask [_ColorMask]

		Pass
		{
		CGPROGRAM
			#pragma vertex vert_sub
			#pragma fragment frag_mix
			#include "UnityCG.cginc"
			#include "../Common.cginc"
		ENDCG
		}
	}

	FallBack "UI/Default"
}
