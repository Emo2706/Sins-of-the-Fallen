// Made with Amplify Shader Editor v1.9.1.3
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_IceSlider"
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
            
            		float2 voronoihash7( float2 p )
            		{
            			
            			p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
            			return frac( sin( p ) *43758.5453);
            		}
            
            		float voronoi7( float2 v, float time, inout float2 id, inout float2 mr, float smoothness, inout float2 smoothId )
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
            			 		float2 o = voronoihash7( n + g );
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
            
            		float2 voronoihash14( float2 p )
            		{
            			
            			p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
            			return frac( sin( p ) *43758.5453);
            		}
            
            		float voronoi14( float2 v, float time, inout float2 id, inout float2 mr, float smoothness, inout float2 smoothId )
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
            			 		float2 o = voronoihash14( n + g );
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
                float4 color34 = IsGammaSpace() ? float4(0.02195372,0.9659636,1.39772,1) : float4(0.001699204,0.9243123,2.088933,1);
                float Percentage4 = _Porcentaje;
                float4 lerpResult29 = lerp( float4( 1,1,1,1 ) , color34 , Percentage4);
                Gradient gradient33 = NewGradient( 0, 2, 2, float4( 0.8033998, 0.9483002, 0.9622642, 0 ), float4( 0.06274509, 0.7043803, 1, 1 ), 0, 0, 0, 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
                float time7 = ( _Time.y * pow( (0.0 + (Percentage4 - 0.0) * (3.87 - 0.0) / (1.0 - 0.0)) , 2.0 ) );
                float2 voronoiSmoothId7 = 0;
                float2 coords7 = IN.texcoord.xy * 5.36;
                float2 id7 = 0;
                float2 uv7 = 0;
                float voroi7 = voronoi7( coords7, time7, id7, uv7, 0, voronoiSmoothId7 );
                float smoothstepResult9 = smoothstep( 0.0 , 0.24 , voroi7);
                float2 temp_cast_0 = (0.4).xx;
                float time14 = ( _Time.y * (0.0 + (Percentage4 - 0.0) * (0.75 - 0.0) / (1.0 - 0.0)) );
                float2 voronoiSmoothId14 = 0;
                float2 coords14 = IN.texcoord.xy * 5.99;
                float2 id14 = 0;
                float2 uv14 = 0;
                float voroi14 = voronoi14( coords14, time14, id14, uv14, 0, voronoiSmoothId14 );
                float lerpResult16 = lerp( 0.0 , voroi14 , 0.03345004);
                float2 temp_cast_1 = (lerpResult16).xx;
                float2 texCoord22 = IN.texcoord.xy * temp_cast_0 + temp_cast_1;
                float temp_output_3_0_g2 = ( tex2D( _TextureSample1, texCoord22 ).r - ( 1.0 - (0.0 + (_Porcentaje - 0.0) * (0.6470588 - 0.0) / (1.0 - 0.0)) ) );
                float4 lerpResult26 = lerp( saturate( ( tex2D( _TextureSample0, uv_TextureSample0 ) * lerpResult29 ) ) , SampleGradient( gradient33, smoothstepResult9 ) , saturate( ( temp_output_3_0_g2 / fwidth( temp_output_3_0_g2 ) ) ));
                

                half4 color = saturate( lerpResult26 );

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
Node;AmplifyShaderEditor.OneMinusNode;1;-33.34406,-435.4456;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;2;-259.3441,-505.4456;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;0.6470588;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;3;-697.3439,421.5544;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;4;-449.3781,-588.4738;Inherit;False;Percentage;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;5;-1133.278,424.4261;Inherit;False;4;Percentage;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-566.3441,433.5544;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VoronoiNode;7;-434.3441,340.5544;Inherit;True;0;0;1;0;1;False;1;False;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;43.26;False;2;FLOAT;5.36;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.GradientSampleNode;8;58.65596,203.5544;Inherit;True;2;0;OBJECT;;False;1;FLOAT;0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;9;-226.3441,334.5544;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.24;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;10;-937.8269,409.9366;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;3.87;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;11;-756.677,526.4746;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-1722.76,26.58069;Inherit;False;Constant;_Float2;Float 2;1;0;Create;True;0;0;0;False;0;False;5.99;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-1589.76,-164.4193;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VoronoiNode;14;-1480.76,-137.4193;Inherit;True;0;0;1;0;1;False;1;False;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.RangedFloatNode;15;-1543.768,-261.5136;Inherit;False;Constant;_Float1;Float 1;2;0;Create;True;0;0;0;False;0;False;0.03345004;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;16;-1206.676,-236.7528;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;17;-1774.76,-229.4193;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;18;-2103.159,-90.47929;Inherit;False;4;Percentage;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;19;-1890.159,-94.47929;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;0.75;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;20;-36.15784,-312.5714;Inherit;True;Step Antialiasing;-1;;2;2a825e80dfb3290468194f83380797bd;0;2;1;FLOAT;0.09;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;21;-419.5988,-299.1806;Inherit;True;Property;_TextureSample1;Texture Sample 1;1;0;Create;True;0;0;0;False;0;False;-1;1a45dcb16fc1e65469cfb59ba66de9db;1a45dcb16fc1e65469cfb59ba66de9db;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;22;-680.8339,-252.3729;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;23;-880.7219,-306.6019;Inherit;False;Constant;_Float0;Float 0;2;0;Create;True;0;0;0;False;0;False;0.4;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;28;-995.6289,294.1093;Inherit;False;4;Percentage;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;29;-691.554,208.4715;Inherit;True;3;0;COLOR;1,1,1,1;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;30;-726.6969,3.395599;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;7292e95e35d900f4388d9510a0959dc7;4a804d1fc0792514e82a9f0a26822e0d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-208.6539,-17.7;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;32;54.38666,-30.2719;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GradientNode;33;-231.5441,251.2544;Inherit;False;0;2;2;0.8033998,0.9483002,0.9622642,0;0.06274509,0.7043803,1,1;1,0;1,1;0;1;OBJECT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;815.2858,-104.8708;Float;False;True;-1;2;ASEMaterialInspector;0;3;S_IceSlider;5056123faa0c79b47ab6ad7e8bf059a4;True;Default;0;0;Default;2;False;True;3;1;False;;10;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;True;True;True;True;True;0;True;_ColorMask;False;False;False;False;False;False;False;True;True;0;True;_Stencil;255;True;_StencilReadMask;255;True;_StencilWriteMask;0;True;_StencilComp;0;True;_StencilOp;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;2;False;;True;0;True;unity_GUIZTestMode;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;0;;0;0;Standard;0;0;1;True;False;;False;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-947.1229,-499.5674;Inherit;False;Property;_Porcentaje;Porcentaje;2;0;Create;True;0;0;0;False;0;False;0;0.6687683;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;27;566.1953,-121.1409;Inherit;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;34;-1053.075,62.53439;Inherit;False;Constant;_Color0;Color 0;3;1;[HDR];Create;True;0;0;0;False;0;False;0.02195372,0.9659636,1.39772,1;1,0.3632075,0.3632075,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;26;315.6476,-128.1437;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
WireConnection;1;0;2;0
WireConnection;2;0;24;0
WireConnection;4;0;24;0
WireConnection;6;0;3;0
WireConnection;6;1;11;0
WireConnection;7;1;6;0
WireConnection;8;0;33;0
WireConnection;8;1;9;0
WireConnection;9;0;7;0
WireConnection;10;0;5;0
WireConnection;11;0;10;0
WireConnection;13;0;17;0
WireConnection;13;1;19;0
WireConnection;14;1;13;0
WireConnection;14;2;12;0
WireConnection;16;1;14;0
WireConnection;16;2;15;0
WireConnection;19;0;18;0
WireConnection;20;1;1;0
WireConnection;20;2;21;0
WireConnection;21;1;22;0
WireConnection;22;0;23;0
WireConnection;22;1;16;0
WireConnection;29;1;34;0
WireConnection;29;2;28;0
WireConnection;31;0;30;0
WireConnection;31;1;29;0
WireConnection;32;0;31;0
WireConnection;0;0;27;0
WireConnection;27;0;26;0
WireConnection;26;0;32;0
WireConnection;26;1;8;0
WireConnection;26;2;20;0
ASEEND*/
//CHKSM=BB80271077E6D818091D09FEA87F63852C3533D6