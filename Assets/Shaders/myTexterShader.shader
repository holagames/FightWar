Shader "Custom/myTexterShader" {
	Properties {
	    _Color ("Main Color", Color) = (0,0,0)
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque"  "LightMode" = "ForwardBase"}
		LOD 200
		Lighting Off

		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		fixed3 _Color; 
		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = 1;
			o.Albedo+=_Color.rgb;
			
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
