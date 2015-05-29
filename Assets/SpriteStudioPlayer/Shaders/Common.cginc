struct appdata_t
{
	float4 vertex   : POSITION;
	float4 color    : COLOR;
	float2 texcoord : TEXCOORD0;
};

struct v2f
{
	float4 vertex   : SV_POSITION;
	fixed4 color    : COLOR;
	half2 texcoord  : TEXCOORD0;
};

fixed4 _Color;
sampler2D _MainTex;

v2f vert(appdata_t IN)
{
	v2f OUT;
	OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
	OUT.texcoord = IN.texcoord;
#ifdef UNITY_HALF_TEXEL_OFFSET
	OUT.vertex.xy += (_ScreenParams.zw-1.0)*float2(-1,1);
#endif
	OUT.color = IN.color * _Color;
	return OUT;
}

fixed4 frag(v2f IN) : SV_Target
{
	half4 color = tex2D(_MainTex, IN.texcoord) * IN.color;
	clip (color.a - 0.01);
	return color;
}