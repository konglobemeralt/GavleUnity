Shader "Outlined/Silhouetted Diffuse" {
	
		Properties
		{
			
			[NoScaleOffset] _MainTex("Texture", 2D) = "black" {}
		}
			SubShader
		{
			//ZTest Always
			Pass
		{ 

			
			CGPROGRAM
			// use "vert" function as the vertex shader
#pragma vertex vert
			// use "frag" function as the pixel (fragment) shader
#pragma fragment frag

			// vertex shader inputs
		struct appdata
		{
			float4 vertex : POSITION; // vertex position
			float2 uv : TEXCOORD0; // texture coordinate
		};

		// vertex shader outputs ("vertex to fragment")
		struct v2f
		{
			float2 uv : TEXCOORD0; // texture coordinate
			float4 vertex : SV_POSITION; // clip space position
		};

		// vertex shader
		v2f vert(appdata v)
		{
			v2f o;
			// transform position to clip space
			// (multiply with model*view*projection matrix)
			o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
			// just pass the texture coordinate
			o.uv = v.uv;
			return o;
		}

		// texture we will sample
		sampler2D _MainTex;

		// pixel shader; returns low precision ("fixed4" type)
		// color ("SV_Target" semantic)
		fixed4 frag(v2f i) : SV_Target
		{
			// sample texture and return it
			fixed4 col = tex2D(_MainTex, i.uv);
		return col;
		}
			ENDCG
		}
		}
	}

//Shader "Outlined/Silhouetted Diffuse" {
//	Properties{
//		_Color("Main Color", Color) = (.5,.5,.5,1)
//		_OutlineColor("Outline Color", Color) = (0,1,0,1)
//		_Outline("Outline width", Range(0.0, 0.3)) = .005
//		_MainTex("Base (RGB)", 2D) = "white" { }
//	}
//
//		CGINCLUDE
//#include "UnityCG.cginc"
//
//	struct appdata {
//		float4 vertex : POSITION;
//		float3 normal : NORMAL;
//	};
//
//	struct v2f {
//		float4 pos : POSITION;
//		float4 color : COLOR;
//	};
//
//	uniform float _Outline;
//	uniform float4 _OutlineColor;
//
//	v2f vert(appdata v) {
//		// just make a copy of incoming vertex data but scaled according to normal direction
//		v2f o;
//		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
//
//
//		o.color = _OutlineColor;
//		return o;
//	}
//	ENDCG
//
//		SubShader{
//		Tags{ "Queue" = "Transparent" }
//
//		// note that a vertex shader is specified here but its using the one above
//		Pass{
//		Name "OUTLINE"
//		Tags{ "LightMode" = "Always" }

//		ColorMask RGB // alpha not used
//
//					  // you can choose what kind of blending mode you want for the outline
//		Blend SrcAlpha OneMinusSrcAlpha // Normal
//										//Blend One One // Additive
//										//Blend One OneMinusDstColor // Soft Additive
//										//Blend DstColor Zero // Multiplicative
//										//Blend DstColor SrcColor // 2x Multiplicative
//
//		CGPROGRAM
//#pragma vertex vert
//#pragma fragment frag
//
//		half4 frag(v2f i) :COLOR{
//		return i.color;
//	}
//		ENDCG
//	}
//
//		Pass{
//		Name "BASE"
//		ZWrite On
//		ZTest LEqual
//		Blend SrcAlpha OneMinusSrcAlpha
//		Material{
//		Diffuse[_Color]
//		Ambient[_Color]
//	}
//		Lighting On
//		SetTexture[_MainTex]{
//		ConstantColor[_Color]
//		Combine texture * constant
//	}
//		SetTexture[_MainTex]{
//		Combine previous * primary DOUBLE
//	}
//	}
//	}
//
//		SubShader{
//		Tags{ "Queue" = "Transparent" }
//
//		Pass{
//		Name "OUTLINE"
//		Tags{ "LightMode" = "Always" }
//		Cull Front
//		ZWrite Off
//		ZTest Always
//		ColorMask RGB
//
//		// you can choose what kind of blending mode you want for the outline
//		Blend SrcAlpha OneMinusSrcAlpha // Normal
//										//Blend One One // Additive
//										//Blend One OneMinusDstColor // Soft Additive
//										//Blend DstColor Zero // Multiplicative
//										//Blend DstColor SrcColor // 2x Multiplicative
//
//										//		CGPROGRAM
//										//#pragma vertex vert
//										//#pragma exclude_renderers gles xbox360 ps3
//										//		ENDCG
//		SetTexture[_MainTex]{ combine primary }
//	}
//
//		Pass{
//		Name "BASE"
//		ZWrite On
//		ZTest LEqual
//		Blend SrcAlpha OneMinusSrcAlpha
//		Material{
//		Diffuse[_Color]
//		Ambient[_Color]
//	}
//		Lighting On
//		SetTexture[_MainTex]{
//		ConstantColor[_Color]
//		Combine texture * constant
//	}
//		SetTexture[_MainTex]{
//		Combine previous * primary DOUBLE
//	}
//	}
//	}
//
//		Fallback "Diffuse"
//}