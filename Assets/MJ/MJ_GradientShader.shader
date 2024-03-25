Shader "Custom/SpriteGradient" {
    Properties {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Top Color", Color) = (1,1,1,1) // Change "Left Color" to "Top Color"
        _Color2 ("Bottom Color", Color) = (1,1,1,1) // Change "Right Color" to "Bottom Color"
        _Scale ("Scale", Float) = 1
        // Unused UI properties...
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
    }
    
    SubShader {
        Tags {"Queue"="Background"  "IgnoreProjector"="True"}
        LOD 100
        ZWrite On
        
        Pass {
            CGPROGRAM
            #pragma vertex vert  
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            fixed4 _Color;
            fixed4 _Color2;
            fixed  _Scale;
            
            struct v2f {
                float4 pos : SV_POSITION;
                fixed4 col : COLOR;
            };
            
            v2f vert (appdata_full v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos (v.vertex);
                o.col = lerp(_Color, _Color2, v.texcoord.y ); // Change from v.texcoord.x to v.texcoord.y for vertical gradient
                return o;
            }
            
            float4 frag (v2f i) : COLOR {
                float4 c = i.col;
                c.a = 1;
                return c;
            }
            ENDCG
        }
    }
}
