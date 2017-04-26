Shader "iPhone/SimpleAdditive" {
Properties {
	_MainTex ("Particle Texture", 2D) = "white" {}
}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha One
	ColorMask RGB
	Cull Off Lighting Off ZWrite Off Fog { Mode Off }

	
	SubShader {
		Pass {
			SetTexture [_MainTex] {
				combine texture
			}
		}
	}
	
}
}
