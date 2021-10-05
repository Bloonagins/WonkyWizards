Shader "TextTools/Outline" 
{
    Properties 
	{
	    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	}

    SubShader
	{
	    Tags { "RenderType" = "Opaque" }
		
		Pass
		{  
		    CGPROGRAM
			
			#pragma vertex   vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

            struct in_t
			{
			    float4 pos : POSITION;
				float2 uv0 : TEXCOORD0;
				float2 uv1 : TEXCOORD1;
				float2 uv2 : TEXCOORD2;
			};

            struct out_t 
			{
			    float4 pos : SV_POSITION;
				float2 uv0 : TEXCOORD0;
				float2 uv1 : TEXCOORD1;
				float2 uv2 : TEXCOORD2;
			};

            sampler2D _MainTex;
			float4    _MainTex_ST;
			
			out_t vert(in_t i)
			{
			    out_t o;
			
			    o.pos = UnityObjectToClipPos(i.pos);				
				o.uv0 = i.uv0;
				o.uv1 = i.uv1;
				o.uv2 = i.uv2;

                return o;
			}
			
			fixed4 frag(out_t i): SV_Target
			{
			    i.uv0.y = ((i.uv1.x / i.uv1.y) * i.uv2.y + i.uv2.x) * _MainTex_ST.y;
			
			    return tex2D(_MainTex, i.uv0);
			}

		    ENDCG
		}
	}
}