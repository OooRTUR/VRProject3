// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Nature/HunterFoxLeavesUnlitShader"
{
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_CutoffTexture("Texture",2D) = "white"{}
	    _Cutoff("Alpha cutoff", Range(0,1)) = 0.5
		_Distance ("Distance", Range(0.001, 500)) = 10
	}
	SubShader{
		Pass
		{
			Tags {"Queue" = "Transparent" "RenderType" = "Transparent" }
			ZWrite off
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM

			#pragma vertex vertexFunction
			#pragma fragment fragmentFunction

			#include "UnityCG.cginc"
			float4 _Color;
			sampler2D _MainTex;
			sampler2D _CutoffTexture;
			float _Cutoff;
			float _Distance;

			//в структурах объявляем переменные, шейдерного языка
			struct appdata {
				float4 vertex: POSITION; //тип название: привязка
				float2 uv : TEXCOORD0;
			};
			struct v2f {
				float4 position : SV_POSITION;
				float2 uv: TEXCOORD0;
				float4 worldPos : TEXCOORD1;
			};
			//объявлем переменные, посредством значений которых будем редактировать переменные
			//в шейдере
			
			
			//vertex function - build it
			v2f vertexFunction(appdata IN) {
				v2f OUT;

				OUT.position = UnityObjectToClipPos(IN.vertex);
				OUT.uv = IN.uv;
				return OUT;
			}

			//fragment function - color it
			fixed4 fragmentFunction(v2f IN) :SV_Target{
				float4 cutoffCol = tex2D(_CutoffTexture,IN.uv);
				clip(cutoffCol.rgb - _Cutoff);
				
				float4 col = tex2D(_MainTex, IN.uv) + cutoffCol;
				
				return col * _Color;
			}

			ENDCG

		}
	}
			Dependency "OptimizedShader" = "Hidden/Nature/Tree Creator Leaves Fast Optimized"
}