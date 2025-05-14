// Made with Amplify Shader Editor v1.9.1.3
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_FireSliderBar"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0

        _TextureSample0("Texture Sample 0", 2D) = "white" {}
        _TextureSample1("Texture Sample 1", 2D) = "white" {}
        _Porcentaje("Porcentaje", Range( 0 , 1)) = 0
        [HideInInspector] _texcoord( "", 2D ) = "white" {}

    }

    SubShader
    {
		LOD 0

        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }

        Stencil
        {
        	Ref [_Stencil]
        	ReadMask [_StencilReadMask]
        	WriteMask [_StencilWriteMask]
        	Comp [_StencilComp]
        	Pass [_StencilOp]
        }


        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend One OneMinusSrcAlpha
        ColorMask [_ColorMask]

        
        Pass
        {
            Name "Default"
        CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                float4  mask : TEXCOORD2;
                UNITY_VERTEX_OUTPUT_STEREO
                
            };

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;
            float _UIMaskSoftnessX;
            float _UIMaskSoftnessY;

            uniform sampler2D _TextureSample0;
            uniform float4 _TextureSample0_ST;
            uniform float _Porcentaje;
            uniform sampler2D _TextureSample1;
            struct Gradient
            {
            	int type;
            	int colorsLength;
            	int alphasLength;
            	float4 colors[8];
            	float2 alphas[8];
            };
            
            Gradient NewGradient(int type, int colorsLength, int alphasLength, 
            float4 colors0, float4 colors1, float4 colors2, float4 colors3, float4 colors4, float4 colors5, float4 colors6, float4 colors7,
            float2 alphas0, float2 alphas1, float2 alphas2, float2 alphas3, float2 alphas4, float2 alphas5, float2 alphas6, float2 alphas7)
            {
            	Gradient g;
            	g.type = type;
            	g.colorsLength = colorsLength;
            	g.alphasLength = alphasLength;
            	g.colors[ 0 ] = colors0;
            	g.colors[ 1 ] = colors1;
            	g.colors[ 2 ] = colors2;
            	g.colors[ 3 ] = colors3;
            	g.colors[ 4 ] = colors4;
            	g.colors[ 5 ] = colors5;
            	g.colors[ 6 ] = colors6;
            	g.colors[ 7 ] = colors7;
            	g.alphas[ 0 ] = alphas0;
            	g.alphas[ 1 ] = alphas1;
            	g.alphas[ 2 ] = alphas2;
            	g.alphas[ 3 ] = alphas3;
            	g.alphas[ 4 ] = alphas4;
            	g.alphas[ 5 ] = alphas5;
            	g.alphas[ 6 ] = alphas6;
            	g.alphas[ 7 ] = alphas7;
            	return g;
            }
            
            		float2 voronoihash124( float2 p )
            		{
            			
            			p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
            			return frac( sin( p ) *43758.5453);
            		}
            
            		float voronoi124( float2 v, float time, inout float2 id, inout float2 mr, float smoothness, inout float2 smoothId )
            		{
            			float2 n = floor( v );
            			float2 f = frac( v );
            			float F1 = 8.0;
            			float F2 = 8.0; float2 mg = 0;
            			for ( int j = -1; j <= 1; j++ )
            			{
            				for ( int i = -1; i <= 1; i++ )
            			 	{
            			 		float2 g = float2( i, j );
            			 		float2 o = voronoihash124( n + g );
            					o = ( sin( time + o * 6.2831 ) * 0.5 + 0.5 ); float2 r = f - g - o;
            					float d = 0.5 * dot( r, r );
            			 		if( d<F1 ) {
            			 			F2 = F1;
            			 			F1 = d; mg = g; mr = r; id = o;
            			 		} else if( d<F2 ) {
            			 			F2 = d;
            			
            			 		}
            			 	}
            			}
            			return F1;
            		}
            
            float4 SampleGradient( Gradient gradient, float time )
            {
            	float3 color = gradient.colors[0].rgb;
            	UNITY_UNROLL
            	for (int c = 1; c < 8; c++)
            	{
            	float colorPos = saturate((time - gradient.colors[c-1].w) / ( 0.00001 + (gradient.colors[c].w - gradient.colors[c-1].w)) * step(c, (float)gradient.colorsLength-1));
            	color = lerp(color, gradient.colors[c].rgb, lerp(colorPos, step(0.01, colorPos), gradient.type));
            	}
            	#ifndef UNITY_COLORSPACE_GAMMA
            	color = half3(GammaToLinearSpaceExact(color.r), GammaToLinearSpaceExact(color.g), GammaToLinearSpaceExact(color.b));
            	#endif
            	float alpha = gradient.alphas[0].x;
            	UNITY_UNROLL
            	for (int a = 1; a < 8; a++)
            	{
            	float alphaPos = saturate((time - gradient.alphas[a-1].y) / ( 0.00001 + (gradient.alphas[a].y - gradient.alphas[a-1].y)) * step(a, (float)gradient.alphasLength-1));
            	alpha = lerp(alpha, gradient.alphas[a].x, lerp(alphaPos, step(0.01, alphaPos), gradient.type));
            	}
            	return float4(color, alpha);
            }
            
            		float2 voronoihash29( float2 p )
            		{
            			
            			p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
            			return frac( sin( p ) *43758.5453);
            		}
            
            		float voronoi29( float2 v, float time, inout float2 id, inout float2 mr, float smoothness, inout float2 smoothId )
            		{
            			float2 n = floor( v );
            			float2 f = frac( v );
            			float F1 = 8.0;
            			float F2 = 8.0; float2 mg = 0;
            			for ( int j = -1; j <= 1; j++ )
            			{
            				for ( int i = -1; i <= 1; i++ )
            			 	{
            			 		float2 g = float2( i, j );
            			 		float2 o = voronoihash29( n + g );
            					o = ( sin( time + o * 6.2831 ) * 0.5 + 0.5 ); float2 r = f - g - o;
            					float d = 0.5 * dot( r, r );
            			 		if( d<F1 ) {
            			 			F2 = F1;
            			 			F1 = d; mg = g; mr = r; id = o;
            			 		} else if( d<F2 ) {
            			 			F2 = d;
            			
            			 		}
            			 	}
            			}
            			return F1;
            		}
            

            
            v2f vert(appdata_t v )
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

                

                v.vertex.xyz +=  float3( 0, 0, 0 ) ;

                float4 vPosition = UnityObjectToClipPos(v.vertex);
                OUT.worldPosition = v.vertex;
                OUT.vertex = vPosition;

                float2 pixelSize = vPosition.w;
                pixelSize /= float2(1, 1) * abs(mul((float2x2)UNITY_MATRIX_P, _ScreenParams.xy));

                float4 clampedRect = clamp(_ClipRect, -2e10, 2e10);
                float2 maskUV = (v.vertex.xy - clampedRect.xy) / (clampedRect.zw - clampedRect.xy);
                OUT.texcoord = v.texcoord;
                OUT.mask = float4(v.vertex.xy * 2 - clampedRect.xy - clampedRect.zw, 0.25 / (0.25 * half2(_UIMaskSoftnessX, _UIMaskSoftnessY) + abs(pixelSize.xy)));

                OUT.color = v.color * _Color;
                return OUT;
            }

            fixed4 frag(v2f IN ) : SV_Target
            {
                //Round up the alpha color coming from the interpolator (to 1.0/256.0 steps)
                //The incoming alpha could have numerical instability, which makes it very sensible to
                //HDR color transparency blend, when it blends with the world's texture.
                const half alphaPrecision = half(0xff);
                const half invAlphaPrecision = half(1.0/alphaPrecision);
                IN.color.a = round(IN.color.a * alphaPrecision)*invAlphaPrecision;

                float2 uv_TextureSample0 = IN.texcoord.xy * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
                float4 color140 = IsGammaSpace() ? float4(0.9339623,0.2775454,0.2775454,1) : float4(0.8562991,0.06260893,0.06260893,1);
                float Percentage131 = _Porcentaje;
                float4 lerpResult142 = lerp( float4( 1,1,1,1 ) , color140 , Percentage131);
                Gradient gradient121 = NewGradient( 0, 2, 2, float4( 1, 0.9238702, 0, 0 ), float4( 1, 0.2172453, 0.06274509, 1 ), 0, 0, 0, 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
                float time124 = ( _Time.y * pow( (0.0 + (Percentage131 - 0.0) * (3.87 - 0.0) / (1.0 - 0.0)) , 2.0 ) );
                float2 voronoiSmoothId124 = 0;
                float2 coords124 = IN.texcoord.xy * 5.36;
                float2 id124 = 0;
                float2 uv124 = 0;
                float voroi124 = voronoi124( coords124, time124, id124, uv124, 0, voronoiSmoothId124 );
                float smoothstepResult125 = smoothstep( 0.0 , 0.24 , voroi124);
                float2 temp_cast_0 = (0.4).xx;
                float time29 = ( _Time.y * (0.0 + (Percentage131 - 0.0) * (0.75 - 0.0) / (1.0 - 0.0)) );
                float2 voronoiSmoothId29 = 0;
                float2 coords29 = IN.texcoord.xy * 5.99;
                float2 id29 = 0;
                float2 uv29 = 0;
                float voroi29 = voronoi29( coords29, time29, id29, uv29, 0, voronoiSmoothId29 );
                float lerpResult35 = lerp( 0.0 , voroi29 , 0.03345004);
                float2 temp_cast_1 = (lerpResult35).xx;
                float2 texCoord36 = IN.texcoord.xy * temp_cast_0 + temp_cast_1;
                float temp_output_3_0_g2 = ( tex2D( _TextureSample1, texCoord36 ).r - ( 1.0 - (0.0 + (_Porcentaje - 0.0) * (0.6470588 - 0.0) / (1.0 - 0.0)) ) );
                float4 lerpResult38 = lerp( saturate( ( tex2D( _TextureSample0, uv_TextureSample0 ) * saturate( lerpResult142 ) ) ) , SampleGradient( gradient121, smoothstepResult125 ) , saturate( ( temp_output_3_0_g2 / fwidth( temp_output_3_0_g2 ) ) ));
                

                half4 color = saturate( lerpResult38 );

                #ifdef UNITY_UI_CLIP_RECT
                half2 m = saturate((_ClipRect.zw - _ClipRect.xy - abs(IN.mask.xy)) * IN.mask.zw);
                color.a *= m.x * m.y;
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif

                color.rgb *= color.a;

                return color;
            }
        ENDCG
        }
    }
    CustomEditor "ASEMaterialInspector"
	
	Fallback Off
}
/*ASEBEGIN
Version=19103
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;167;503.581,-204.3318;Float;False;True;-1;2;ASEMaterialInspector;0;3;S_FireSliderBar;5056123faa0c79b47ab6ad7e8bf059a4;True;Default;0;0;Default;2;False;True;3;1;False;;10;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;True;True;True;True;True;0;True;_ColorMask;False;False;False;False;False;False;False;True;True;0;True;_Stencil;255;True;_StencilReadMask;255;True;_StencilWriteMask;0;True;_StencilComp;0;True;_StencilOp;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;2;False;;True;0;True;unity_GUIZTestMode;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;0;;0;0;Standard;0;0;1;True;False;;False;0
Node;AmplifyShaderEditor.OneMinusNode;120;-347.1541,-628.0347;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;123;-573.1541,-698.0347;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;0.6470588;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;127;-1011.154,228.9653;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;131;-763.1881,-781.0629;Inherit;False;Percentage;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;130;-1447.088,231.837;Inherit;False;131;Percentage;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;128;-880.1541,240.9653;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VoronoiNode;124;-748.1541,147.9653;Inherit;True;0;0;1;0;1;False;1;False;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;43.26;False;2;FLOAT;5.36;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.GradientSampleNode;122;-255.1541,10.96533;Inherit;True;2;0;OBJECT;;False;1;FLOAT;0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;125;-540.1541,141.9653;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.24;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;132;-1251.637,217.3475;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;3.87;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;136;-1070.487,333.8855;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-2036.57,-166.0084;Inherit;False;Constant;_Float2;Float 2;1;0;Create;True;0;0;0;False;0;False;5.99;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;-1903.57,-357.0084;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VoronoiNode;29;-1794.57,-330.0084;Inherit;True;0;0;1;0;1;False;1;False;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.RangedFloatNode;27;-1857.578,-454.1027;Inherit;False;Constant;_Float1;Float 1;2;0;Create;True;0;0;0;False;0;False;0.03345004;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;35;-1520.486,-429.3419;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;31;-2088.57,-422.0084;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;138;-2416.969,-283.0684;Inherit;False;131;Percentage;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;139;-2203.969,-287.0684;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;0.75;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;40;-349.9679,-505.1605;Inherit;True;Step Antialiasing;-1;;2;2a825e80dfb3290468194f83380797bd;0;2;1;FLOAT;0.09;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;37;-733.4088,-491.7697;Inherit;True;Property;_TextureSample1;Texture Sample 1;1;0;Create;True;0;0;0;False;0;False;-1;1a45dcb16fc1e65469cfb59ba66de9db;1a45dcb16fc1e65469cfb59ba66de9db;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;36;-994.644,-444.962;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;118;-1194.532,-499.191;Inherit;False;Constant;_Float0;Float 0;2;0;Create;True;0;0;0;False;0;False;0.4;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;119;-1260.933,-692.1565;Inherit;False;Property;_Porcentaje;Porcentaje;2;0;Create;True;0;0;0;False;0;False;0;0.5632729;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;144;-700.7957,-15.44379;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;38;-41.06419,-303.5721;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;145;205.0241,-281.5921;Inherit;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;143;-1309.439,101.5202;Inherit;False;131;Percentage;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;142;-1005.364,15.88245;Inherit;True;3;0;COLOR;1,1,1,1;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;6;-1040.507,-189.1935;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;7292e95e35d900f4388d9510a0959dc7;7292e95e35d900f4388d9510a0959dc7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;141;-522.4639,-210.2891;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;146;-259.4234,-222.861;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GradientNode;121;-545.3542,58.66533;Inherit;False;0;2;2;1,0.9238702,0,0;1,0.2172453,0.06274509,1;1,0;1,1;0;1;OBJECT;0
Node;AmplifyShaderEditor.ColorNode;140;-1366.885,-130.0547;Inherit;False;Constant;_Color0;Color 0;3;0;Create;True;0;0;0;False;0;False;0.9339623,0.2775454,0.2775454,1;1,0.3632075,0.3632075,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
WireConnection;167;0;145;0
WireConnection;120;0;123;0
WireConnection;123;0;119;0
WireConnection;131;0;119;0
WireConnection;128;0;127;0
WireConnection;128;1;136;0
WireConnection;124;1;128;0
WireConnection;122;0;121;0
WireConnection;122;1;125;0
WireConnection;125;0;124;0
WireConnection;132;0;130;0
WireConnection;136;0;132;0
WireConnection;33;0;31;0
WireConnection;33;1;139;0
WireConnection;29;1;33;0
WireConnection;29;2;30;0
WireConnection;35;1;29;0
WireConnection;35;2;27;0
WireConnection;139;0;138;0
WireConnection;40;1;120;0
WireConnection;40;2;37;0
WireConnection;37;1;36;0
WireConnection;36;0;118;0
WireConnection;36;1;35;0
WireConnection;144;0;142;0
WireConnection;38;0;146;0
WireConnection;38;1;122;0
WireConnection;38;2;40;0
WireConnection;145;0;38;0
WireConnection;142;1;140;0
WireConnection;142;2;143;0
WireConnection;141;0;6;0
WireConnection;141;1;144;0
WireConnection;146;0;141;0
ASEEND*/
//CHKSM=0E3DAC24688A20A4F1A7B90332581C68818F4768