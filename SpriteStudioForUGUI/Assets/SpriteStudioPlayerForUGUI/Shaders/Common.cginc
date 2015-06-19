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

// 頂点シェーダーの基本
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

// 加算合成
v2f vert_add(appdata_t IN)
{
    // XXX: なぜか頂点のαが0.2位減ってる感じがする
	v2f OUT = vert( IN );
    OUT.color.rgb = OUT.color.rgb * (IN.color.a + 0.2);
    return OUT;
}

// 減算合成
v2f vert_sub(appdata_t IN)
{
    return vert_add( IN );
}


// 通常のフラグメントシェーダー
fixed4 frag(v2f IN) : SV_Target
{
	half4 color = tex2D(_MainTex, IN.texcoord) * IN.color;
	clip (color.a - 0.01);
	return color;
}

// 加算
fixed4 frag_add(v2f IN) : SV_Target
{
	half4 color = tex2D(_MainTex, IN.texcoord) + IN.color;
	clip (color.a - 0.01);
	return color;
}


// 減算
fixed4 frag_sub(v2f IN) : SV_Target
{
	half4 color = tex2D(_MainTex, IN.texcoord) - IN.color;
	clip (color.a - 0.01);
	return color;
}

// αブレンド
fixed4 frag_mix(v2f IN) : SV_Target
{
	half4 color = tex2D(_MainTex, IN.texcoord);
    color.rgb = color.rgb * (1 - IN.color.a) + IN.color.rgb * IN.color.a;
	clip (color.a - 0.01);
	return color;
}
