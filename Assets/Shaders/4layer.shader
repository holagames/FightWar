Shader "Custom/4layer" {
	Properties {
		_Splat0 ("Layer 1 r", 2D) = "white" {}
		_Splat1 ("Layer 2 g", 2D) = "white" {}
		_Splat2 ("Layer 3 b", 2D) = "white" {}
		_Control ("Control (RGB)", 2D) = "white" {}
	}
                
	SubShader {
			Tags {
			   "SplatCount" = "4"
			   "RenderType" = "Opaque"
			}
		CGPROGRAM
		#pragma surface surf Lambert 
		
		struct Input {
			float2 uv_Control : TEXCOORD0;
			float2 uv_Splat0 : TEXCOORD1;
			float2 uv_Splat1 : TEXCOORD2;
			float2 uv_Splat2 : TEXCOORD3;
		};
		 
		sampler2D _Control;
		sampler2D _Splat0,_Splat1,_Splat2;
		 
		void surf (Input IN, inout SurfaceOutput o) {
			fixed3 splat_control = tex2D (_Control, IN.uv_Control).rgb;
				
			fixed3 lay1 = tex2D (_Splat0, IN.uv_Splat0);
			fixed3 lay2 = tex2D (_Splat1, IN.uv_Splat1);
			fixed3 lay3 = tex2D (_Splat2, IN.uv_Splat2);
			
			o.Albedo = lay1.rgb;
			o.Albedo = lerp(o.Albedo,lay2,splat_control.g);
			o.Albedo = lerp(o.Albedo,lay3,splat_control.b);
			o.Alpha = 1.0f;
			//o.Albedo.rgb = (lay1 * splat_control.r + lay2 * splat_control.g + lay3 * splat_control.b + lay4 * splat_control.a);
		}
		ENDCG 
	}
	// Fallback to Diffuse
	Fallback "Diffuse"
	

}
