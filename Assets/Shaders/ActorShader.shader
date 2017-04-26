Shader "Custom/ActorShader" {

	 Properties  
    {  
        _Color ("Main Color", Color) = (1,1,1,1)  
        _MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}  
        _RimColor ("Rim Color", Color) = (1,1,1,1)  
    }  
  
    SubShader  
    {  
        Tags { "RenderType"="Opaque" }  
        LOD 400  
        Lighting Off
        CGPROGRAM  
        #pragma surface surf Lambert  
  
        sampler2D _MainTex;  
        sampler2D _BumpMap;   
        fixed4 _Color;  
        float4 _RimColor;  
  
        struct Input  
        {  
            float2 uv_MainTex;  
            float2 uv_BumpMap;  
            float3 viewDir;  
        };  
  
        void surf (Input IN, inout SurfaceOutput o)  
        {  
            fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);  
            fixed4 c = tex * (_Color+(tex.a*0.5));  
            o.Albedo = c.rgb;
            o.Emission =c.rgb + tex.a*_RimColor.rgb*_RimColor.rgb;  
        }  
          
        ENDCG  
       } 
}
