Shader "T4MShaders/ShaderModel2/Diffuse/new UV" {
Properties {
	_Splat0 ("Layer 1 R", 2D) = "white" {}
	_Splat1 ("Layer 2 G", 2D) = "white" {}
	_Splat2 ("Layer 3 B", 2D) = "white" {}
	_Control ("Control (RGB)", 2D) = "white" {}
	_MixValue ("MixValue (Range)",Range(0,1)) = 0.5 
}
                
SubShader {
	Tags {
   "Queue"="Geometry+1"
   "IgnoreProjector"="False" 
   "SplatCount" = "2"
   "RenderType" = "Opaque"
	}
CGPROGRAM
#pragma surface surf Lambert 
#pragma exclude_renderers xbox360 ps3

struct Input {
	float2 uv_Control : TEXCOORD0;
	float2 uv_Splat0 : TEXCOORD1;
	float2 uv_Splat1 : TEXCOORD2;
	float2 uv_Splat2 : TEXCOORD3;
};
 
sampler2D _Control;
sampler2D _Splat0,_Splat1,_Splat2;
float _MixValue;
 
void surf (Input IN, inout SurfaceOutput o) {
	fixed3 splat_control = tex2D (_Control, IN.uv_Control).rgb;
		
	fixed4 lay1 = tex2D (_Splat0, IN.uv_Splat0);
	fixed4 lay2 = tex2D (_Splat1, IN.uv_Splat1);
	fixed4 lay3 = tex2D (_Splat2, IN.uv_Splat1);
	o.Alpha = 0.0;
	o.Albedo.rgb = lay1 * splat_control.r, lay2 * splat_control.g+lay3* lay2 * splat_control.b;
	//o.Albedo.rgb = lerp(lay1 * splat_control.r, lay2 * splat_control.b,_MixValue);
}
ENDCG 
}
// Fallback to Diffuse
Fallback "Diffuse"
}
