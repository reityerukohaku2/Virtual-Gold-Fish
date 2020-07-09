Shader "Hidden/HeightToNormal"
{
	Properties
	{
		_InputTex("InputTex", 2D) = "defaulttexture" {}
		_DeltaUV("DeltaUV", float) = 3
	}

		SubShader
		{
			Cull Off
			ZWrite Off
			ZTest Always

			Pass
			{
				CGPROGRAM										//開始
				#pragma vertex CustomRenderTextureVertexShader	//頂点シェーダーはこれを使う
				#pragma fragment frag							//フラグメントシェーダーはfragで始まる

				#include "UnityCG.cginc"
				#include "Lighting.cginc"
				#include "UnityCustomRenderTexture.cginc"		//インクルード

				float _DeltaUV;
			sampler2D _InputTex;
			//float4 texSize = _InputTex_TexelSize;

			float4 frag(v2f_customrendertexture i) : SV_Target
			{
				float2 uv = i.globalTexcoord;

				// 1pxあたりの単位を計算する
				float2 texSize = (512, 512);
				float du = - 1.0 / texSize.x * _DeltaUV;
				float dv = - 1.0 / texSize.y * _DeltaUV;
				float3 duv = float3(du, dv, 0);

				// 現在の位置のテクセルをフェッチ
				float3 c = tex2D(_InputTex, float3(uv.x, uv.y, 0));

				float dyx = (tex2D(_InputTex, float2(uv.x + du, uv.y)) - tex2D(_InputTex, float2(uv.x - du, uv.y))) / 2.0f;
				float dyz = (tex2D(_InputTex, float2(uv.x, uv.y + dv)) - tex2D(_InputTex, float2(uv.x, uv.y - dv))) / 2.0f;
				float3 tu = (1, dyx, 0);
				float3 tv = (0, dyz, 1);
				//float3 n = normalize(cross(tv, tu));
				float3 n = cross(tv, tu);
				//float3 n = (c.r, c.g, c.b);

				// 現在の状態をテクスチャのR成分に、ひとつ前の（過去の）状態をG成分に書き込む。
				//return float4(n.r / 2.0f + 0.5, n.g / 2.0f + 0.5, n.b / 4.0f + 0.75, 1);
				return float4(dyx / 2.0f + 0.5, dyz / 2.0f + 0.5, 1 / 4.0f + 0.75, 1);
				//return float4(n.r, n.g, n.b, 1);
				//return float4(dyx, dyz, 1, 1);
			}
			ENDCG
		}
	}
}