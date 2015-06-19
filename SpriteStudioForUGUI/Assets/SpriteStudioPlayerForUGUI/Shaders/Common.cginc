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

// ���_�V�F�[�_�[�̊�{
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

// ���Z����
v2f vert_add(appdata_t IN)
{
    // XXX: �Ȃ������_�̃���0.2�ʌ����Ă銴��������
	v2f OUT = vert( IN );
    OUT.color.rgb = OUT.color.rgb * (IN.color.a + 0.2);
    return OUT;
}

// ���Z����
v2f vert_sub(appdata_t IN)
{
    return vert_add( IN );
}


// �ʏ�̃t���O�����g�V�F�[�_�[
fixed4 frag(v2f IN) : SV_Target
{
	half4 color = tex2D(_MainTex, IN.texcoord) * IN.color;
	clip (color.a - 0.01);
	return color;
}

// ���Z
fixed4 frag_add(v2f IN) : SV_Target
{
	half4 color = tex2D(_MainTex, IN.texcoord) + IN.color;
	clip (color.a - 0.01);
	return color;
}


// ���Z
fixed4 frag_sub(v2f IN) : SV_Target
{
	half4 color = tex2D(_MainTex, IN.texcoord) - IN.color;
	clip (color.a - 0.01);
	return color;
}

// ���u�����h
fixed4 frag_mix(v2f IN) : SV_Target
{
	half4 color = tex2D(_MainTex, IN.texcoord);
    color.rgb = color.rgb * (1 - IN.color.a) + IN.color.rgb * IN.color.a;
	clip (color.a - 0.01);
	return color;
}
