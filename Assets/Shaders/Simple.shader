Shader "iPhone/Simple"
{
	Properties
	{
		_MainTex ("Base(RGB)",2D) = "white"{}
		//_Color ("Main Color", Color) = (1,1,1,1)
	}

	SubShader
	{
		Pass
		{
		    Cull OFF
			Lighting OFF
			SetTexture [_MainTex] 
			{
				//constantColor [_Color]
				//combine texture * constant
			}
		}

	}		

}

