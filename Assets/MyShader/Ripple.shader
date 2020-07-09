Shader "Hidden/WaveShader"
{
	Properties
	{
		_S2("PhaseVelocity^2", Range(0.0, 0.5)) = 0.2
		_Atten("Attenuation", Range(0.0, 1.0)) = 0.999
		_DeltaUV("Delta UV", Float) = 3
	}

	SubShader
	{
		Cull Off
		ZWrite Off
		ZTest Always

		//波動方程式
		// (1 / s^2) * (δ^2 * u / (δ * t^2)) = Δu
		//Δはラプラシアンフィルタ
		Pass
		{
			CGPROGRAM										//開始
			#pragma vertex CustomRenderTextureVertexShader	//頂点シェーダーはこれを使う
			#pragma fragment frag							//フラグメントシェーダーはfragで始まる

			#include "UnityCustomRenderTexture.cginc"		//インクルード

			half _S2;				//波動方程式の最初の分母？
			half _Atten;			//波の減衰量
			float _DeltaUV;
			sampler2D _MainTex;

			float4 frag(v2f_customrendertexture i) : SV_Target
			{
				float2 uv = i.globalTexcoord;

				// 1pxあたりの単位を計算する
				float du = 1.0 / _CustomRenderTextureWidth;
				float dv = 1.0 / _CustomRenderTextureHeight;
				float3 duv = float3(du, dv, 0) * _DeltaUV;

				// 現在の位置のテクセルをフェッチ
				float4 c = tex2D(_SelfTexture2D, uv);

				// ラプラシアンフィルタをかける
				// |0  1 0|
				// |1 -4 1|
				// |0  1 0|
				float k = (2.0 * c.r) - c.a;
				float p = (k + _S2 * 
						(tex2D(_SelfTexture2D, uv - duv.zy).r 
						+tex2D(_SelfTexture2D, uv + duv.zy).r 
						+tex2D(_SelfTexture2D, uv - duv.xz).r 
						+tex2D(_SelfTexture2D, uv + duv.xz).r 
						-4.0 * c.r)
					) * _Atten;

				// 現在の状態をテクスチャのR成分に、ひとつ前の（過去の）状態をG成分に書き込む。
				return float4(p, p, p, c.r);
			}
			ENDCG
		}

		Pass
		{
			Name "LeftClick"
			CGPROGRAM
			#pragma vertex CustomRenderTextureVertexShader
			#pragma fragment FragLeftClick

			#include "UnityCustomRenderTexture.cginc"

			float4 FragLeftClick(v2f_customrendertexture i) : SV_Target
			{
				return float4(-1, 0, 0, 0);
			}

			ENDCG
		}
	}
}